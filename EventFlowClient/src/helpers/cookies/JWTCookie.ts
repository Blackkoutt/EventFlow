import Cookies from "js-cookie";

export const setJWTTokenCookie = (token: string): void => {
  Cookies.set("EventFlowJWTCookie", token, {
    expires: 7,
    secure: true,
    //httpOnly: true,
    sameSite: "Strict", // Zapobiega atakom CSRF
  });
};

export const removeJWTTokenCookie = (): void => {
  Cookies.remove("EventFlowJWTCookie");
};
