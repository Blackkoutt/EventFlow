import Cookies from "js-cookie";

const jwtTokenCookieName = "EventFlowJWTCookie";

export const setJWTTokenCookie = (token: string, expirationDate?: Date): void => {
  const expires = expirationDate || new Date(new Date().getTime() + 7 * 24 * 60 * 60 * 1000);

  Cookies.set(jwtTokenCookieName, token, {
    expires: expires,
    secure: true,
    //httpOnly: true,
    sameSite: "Strict", // Zapobiega atakom CSRF
  });
};

export const isJWTTokenCookieExist = () => {
  return Cookies.get(jwtTokenCookieName) !== undefined;
};

export const removeJWTTokenCookie = (): void => {
  Cookies.remove(jwtTokenCookieName);
};
