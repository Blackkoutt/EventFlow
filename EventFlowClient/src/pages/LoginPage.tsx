import Input from "../components/common/forms/Input";
import { useAuth } from "../context/AuthContext";
import { faEnvelope, faKey } from "@fortawesome/free-solid-svg-icons";
import { UserLoginRequest, userLoginSchema } from "../models/create_schemas/auth/UserLoginSchema";
import { SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import googleLogo from "../assets/googleLogo.png";
import fbLogo from "../assets/facebookLogo.png";
import loginHero from "../assets/loginHero.png";
import ExternalLoginButton from "../components/loginpage/ExternalLoginButton";
import Checkbox from "../components/common/Checkbox";
import { Link, useNavigate } from "react-router-dom";
import { useEffect } from "react";

const LoginPage = () => {
  const { authenticated, handleLogin, handleLogout } = useAuth();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    setError,
    setValue,
    formState: { errors, isSubmitting },
  } = useForm<UserLoginRequest>({
    defaultValues: {},
    resolver: zodResolver(userLoginSchema),
  });

  console.log(authenticated);
  useEffect(() => {
    if (authenticated) {
      navigate("/");
    }
  }, [authenticated]);

  const onSubmit: SubmitHandler<UserLoginRequest> = async (data) => {
    await handleLogin(data);
  };
  return (
    <div className="flex flex-row justify-center items-center gap-20 py-16 w-full">
      <div className="flex flex-col justify-center items-start">
        <h2 className="tracking-wide">
          <span className="text-[#2F2F2F] text-4xl font-semibold">Witaj w </span>
          <span className="text-primaryPurple font-extrabold text-5xl">EventFlow!</span>
        </h2>
        <img src={loginHero} alt="Obrazek powitalny logowania" className="object-contain" />
      </div>
      <div className="bg-white min-w-[580px] rounded-md drop-shadow-xl p-10 flex flex-col justify-center items-center gap-10">
        <h3 className="text-black font-bold text-4xl">Zaloguj się</h3>
        <div className="flex flex-col justify-center items-center gap-6 w-full">
          <ExternalLoginButton
            text="Zaloguj się za pomocą konta Google"
            logo={googleLogo}
            onClick={() => {}}
          />
          <ExternalLoginButton
            text="Zaloguj się za pomocą konta Facebook"
            logo={fbLogo}
            onClick={() => {}}
          />
          <div className="flex flex-row justify-between items-center w-full">
            <div className="bg-[#2F2F2F] min-h-[0.5px]" style={{ width: "43%" }}></div>
            <p className="text-[#2F2F2F] text-base">LUB</p>
            <div className="bg-[#2F2F2F] min-h-[0.5px]" style={{ width: "43%" }}></div>
          </div>
          <form
            className="flex flex-col justify-center items-center gap-6 w-full"
            onSubmit={handleSubmit(onSubmit)}
          >
            <Input
              {...register("email")}
              icon={faEnvelope}
              label="Email"
              type="text"
              name="email"
            />
            {errors.email && <div className="text-red-500">{errors.email.message}</div>}

            <Input
              {...register("password")}
              icon={faKey}
              label="Hasło"
              type="password"
              name="password"
            />
            {errors.password && <div className="text-red-500">{errors.password.message}</div>}

            <div className="flex flex-row justify-between items-center w-full">
              <Checkbox color="#7B2CBF" textColor="#2F2F2F" fontSize={16} text="Zapamiętaj mnie" />
              <Link to="/forgot-password" className="text-[#6358DC] text-base">
                Zapomniałeś hasła?
              </Link>
            </div>
            <button
              disabled={isSubmitting}
              className="bg-primaryPurple rounded-md w-full py-5 text-white"
              type="submit"
            >
              {isSubmitting ? "Ładowanie..." : "Zaloguj się"}
            </button>
            {/* {errors.root && <div className="text-red-500">{errors.email.message}</div>} */}
            <div className="flex flex-row justify-center items-center gap-1">
              <p className="text-base text-[#2f2f2f]">Nie masz jeszcze konta? </p>
              <Link to="/sign-up" className="text-[#6358DC] text-base">
                Zarejestruj się
              </Link>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};
export default LoginPage;
