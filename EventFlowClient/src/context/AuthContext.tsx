import { createContext, PropsWithChildren, useContext, useEffect, useState } from "react";
import { User } from "../models/response_models";
import { UserLogin } from "../models/response_models/UserLogin";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { UserLoginRequest } from "../models/create_schemas/auth/UserLoginSchema";
import { jwtDecode } from "jwt-decode";
import {
  getUserFormCookie,
  isJWTTokenCookieExist,
  removeJWTTokenCookie,
  setJWTTokenCookie,
} from "../helpers/cookies/JWTCookie";
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
  activateUser: () => void;
  handleLogout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

type AuthProviderProps = PropsWithChildren;

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [authenticated, setAuthenticated] = useState<boolean | null>(isJWTTokenCookieExist());
  const [currentUser, setCurrentUser] = useState<User | null>(getUserFormCookie());

  // Login in application
  const { data: loginResponse, post: loginUser } = useApi<UserLogin, UserLoginRequest>(
    ApiEndpoint.AuthLogin
  );

  // External login in application via Google
  const { data: loginGoogleResponse, post: loginGoogleUser } = useApi<
    UserLogin,
    ExternalLoginRequest
  >(ApiEndpoint.AuthLoginGoogle);

  // External login in application via Facebook
  const { data: loginFacebookResponse, post: loginFacebookUser } = useApi<
    UserLogin,
    ExternalLoginRequest
  >(ApiEndpoint.AuthLoginFacebook);

  // Activate User
  const { statusCode: activateStatusCode, patch: patchActivateUser } = useApi<
    UserLogin,
    ExternalLoginRequest
  >(ApiEndpoint.AuthActivate);

  // Auth Validation
  const {
    data: validatedUser,
    get: validateUser,
    error: validateUserError,
  } = useApi<User>(ApiEndpoint.AuthValidate);

  // Auth Validation
  useEffect(() => {
    if (isJWTTokenCookieExist()) {
      validateUser({ id: undefined, queryParams: undefined });
    } else {
      setAuthenticated(false);
      setCurrentUser(null);
    }
  }, []);

  useEffect(() => {
    if (validateUserError !== null) {
      setAuthenticated(false);
      setCurrentUser(null);
    } else if (validateUser.length !== 0 && validatedUser[0] !== undefined) {
      setAuthenticated(true);
      const user = validatedUser[0];
      setCurrentUser({
        id: user.id,
        name: user.name,
        surname: user.surname,
        emailAddress: user.emailAddress,
        isVerified: JSON.parse(`${user.isVerified}`.toLowerCase()),
        dateOfBirth: user.dateOfBirth,
        userRoles: user.userRoles,
      });
    }
  }, [validatedUser, validateUserError]);

  // Set Cookie with JWT token if user is Authenticated
  useEffect(() => {
    const loginResponses = [loginResponse, loginGoogleResponse, loginFacebookResponse];

    let token: string | undefined = undefined;
    for (const response of loginResponses) {
      if (response.length > 0) {
        token = response[0].token;
        break;
      }
    }
    console.log("token", token);

    performAuthentication(token);
  }, [loginResponse, loginGoogleResponse, loginFacebookResponse]);

  const performAuthentication = (token?: string) => {
    if (token !== undefined) {
      setAuthenticated(true);
      const decodedToken = jwtDecode(token);
      console.log(decodedToken);
      const userDecodedToken = decodedToken as User;
      let tokenExpirationDate: Date | undefined = undefined;
      if (decodedToken.exp !== undefined) {
        tokenExpirationDate = new Date(decodedToken.exp * 1000);
      }
      setCurrentUser({
        id: userDecodedToken.id,
        name: userDecodedToken.name,
        surname: userDecodedToken.surname,
        emailAddress: userDecodedToken.emailAddress,
        isVerified: JSON.parse(`${userDecodedToken.isVerified}`.toLowerCase()),
        dateOfBirth: userDecodedToken.dateOfBirth,
        userRoles: userDecodedToken.userRoles,
      });
      setJWTTokenCookie(token, tokenExpirationDate);
    }
  };

  const handleLogin = async (loginRequest: UserLoginRequest) => {
    await loginUser({ body: loginRequest });
  };

  const activateUser = async () => {
    await patchActivateUser({ id: undefined, body: undefined });
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
        activateUser,
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
