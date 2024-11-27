import { createContext, PropsWithChildren, useContext, useEffect, useState } from "react";
import { User } from "../models/response_models";
import { UserLogin } from "../models/response_models/UserLogin";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { UserLoginRequest } from "../models/create_schemas/auth/UserLoginSchema";
import { jwtDecode } from "jwt-decode";
import { removeJWTTokenCookie, setJWTTokenCookie } from "../helpers/cookies/JWTCookie";

type AuthContextType = {
  authenticated?: boolean | null;
  currentUser?: User | null;
  handleLogin: (loginRequest: UserLoginRequest) => Promise<void>;
  handleLogout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

type AuthProviderProps = PropsWithChildren;

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [authenticated, setAuthenticated] = useState<boolean | null>();
  const [currentUser, setCurrentUser] = useState<User | null>();

  const {
    data: loginResponse,
    post: loginUser,
    error: loginError,
  } = useApi<UserLogin, UserLoginRequest>(ApiEndpoint.AuthLogin);

  const {
    data: user,
    get: validateUser,
    error: userError,
  } = useApi<User>(ApiEndpoint.AuthValidate);

  useEffect(() => {
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

  useEffect(() => {
    if (loginError === null && loginResponse.length > 0) {
      setAuthenticated(true);
      const token = loginResponse[0].token;
      const decodedToken = jwtDecode(token);
      console.log(decodedToken);

      setJWTTokenCookie(token);
    } else if (loginError !== null) {
      console.log("Error:", loginError);
    }
  }, [loginResponse, loginError]);

  const handleLogin = async (loginRequest: UserLoginRequest) => {
    await loginUser({ body: loginRequest });
  };

  const handleLogout = () => {
    setAuthenticated(false);
    setCurrentUser(null);
    removeJWTTokenCookie();
  };

  return (
    <AuthContext.Provider value={{ authenticated, currentUser, handleLogin, handleLogout }}>
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
