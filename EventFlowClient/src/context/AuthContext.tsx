import { createContext, PropsWithChildren, useContext, useEffect, useState } from "react";
import { User } from "../models/response_models";
import { UserLogin } from "../models/response_models/UserLogin";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { UserLoginRequest } from "../models/create_schemas/auth/UserLoginSchema";
import { jwtDecode } from "jwt-decode";
import { removeJWTTokenCookie, setJWTTokenCookie } from "../helpers/cookies/JWTCookie";
import { ExternalLoginRequest } from "../models/create_schemas/auth/ExternalLoginSchema";
import { ExternalLoginProvider } from "../helpers/enums/ExternalLoginProviders";
import { APIError } from "../models/error/APIError";

type AuthContextType = {
  authenticated?: boolean | null;
  currentUser?: User | null;
  handleLogin: (loginRequest: UserLoginRequest) => Promise<void>;
  handleExternalLogin: (
    request: ExternalLoginRequest,
    provider: ExternalLoginProvider
  ) => Promise<void>;
  performAuthentication: (token?: string, errorMessages?: APIError[]) => void;
  handleLogout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

type AuthProviderProps = PropsWithChildren;

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [authenticated, setAuthenticated] = useState<boolean | null>();
  const [currentUser, setCurrentUser] = useState<User | null>();

  // Login in application
  const {
    data: loginResponse,
    post: loginUser,
    error: loginError,
  } = useApi<UserLogin, UserLoginRequest>(ApiEndpoint.AuthLogin);

  // External login in application via Google
  const {
    data: loginGoogleResponse,
    post: loginGoogleUser,
    error: loginGoogleError,
  } = useApi<UserLogin, ExternalLoginRequest>(ApiEndpoint.AuthLoginGoogle);

  // External login in application via Facebook
  const {
    data: loginFacebookResponse,
    post: loginFacebookUser,
    error: loginFacebookError,
  } = useApi<UserLogin, ExternalLoginRequest>(ApiEndpoint.AuthLoginFacebook);

  // Auth Validation
  const {
    data: user,
    get: validateUser,
    error: userError,
  } = useApi<User>(ApiEndpoint.AuthValidate);

  // Auth Validation
  useEffect(() => {
    // get info from local storage
    validateUser({ id: undefined, queryParams: undefined });
  }, []);

  useEffect(() => {
    console.log("user", user);
    if (user.length == 0) {
      setAuthenticated(false);
    } else {
      setAuthenticated(true);
    }
  }, [user]);

  // Set Cookie with JWT token if user is Authenticated
  useEffect(() => {
    const loginResponses = [
      { response: loginResponse, error: loginError },
      { response: loginGoogleResponse, error: loginGoogleError },
      { response: loginFacebookResponse, error: loginFacebookError },
    ];

    let token: string | undefined = undefined;

    for (const { response, error } of loginResponses) {
      if (error === null && response.length > 0) {
        token = response[0].token;
        break;
      }
    }

    const errorMessages = [loginError, loginGoogleError, loginFacebookError].filter(
      (error) => error !== null
    );

    performAuthentication(token, errorMessages);
  }, [
    loginResponse,
    loginError,
    loginGoogleResponse,
    loginGoogleError,
    loginFacebookResponse,
    loginFacebookError,
  ]);

  const performAuthentication = (token?: string, errorMessages?: APIError[]) => {
    if (token !== undefined) {
      console.log(token);
      setAuthenticated(true);
      const decodedToken = jwtDecode(token);
      console.log(decodedToken);
      setJWTTokenCookie(token);
    } else if (errorMessages !== undefined && errorMessages.length > 0) {
      console.log("Error:", errorMessages[0]);
    }
  };

  const handleLogin = async (loginRequest: UserLoginRequest) => {
    await loginUser({ body: loginRequest });
  };

  const handleExternalLogin = async (
    request: ExternalLoginRequest,
    provider: ExternalLoginProvider
  ) => {
    switch (provider) {
      case ExternalLoginProvider.Google:
        await loginGoogleUser({ body: request });
        break;
      case ExternalLoginProvider.Facebook:
        await loginFacebookUser({ body: request });
        break;
    }
  };

  const handleLogout = () => {
    setAuthenticated(false);
    setCurrentUser(null);
    removeJWTTokenCookie();
  };

  return (
    <AuthContext.Provider
      value={{
        authenticated,
        currentUser,
        handleLogin,
        handleExternalLogin,
        performAuthentication,
        handleLogout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);

  if (context === undefined) {
    throw Error("Use auth must be used in AuthProvider");
  }

  return context;
};
