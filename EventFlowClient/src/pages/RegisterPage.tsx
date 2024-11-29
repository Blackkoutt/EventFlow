import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import {
  UserRegisterRequest,
  userRegisterSchema,
} from "../models/create_schemas/auth/UserRegisterSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import registerHero from "../assets/registerHero.png";
import ExternalLoginButton from "../components/loginpage/ExternalLoginButton";
import googleLogo from "../assets/googleLogo.png";
import fbLogo from "../assets/facebookLogo.png";
import { UserLoginRequest } from "../models/create_schemas/auth/UserLoginSchema";
import Input from "../components/common/forms/Input";
import { faEnvelope, faKey, faCalendar, faUserCircle } from "@fortawesome/free-solid-svg-icons";
import { Link, useNavigate } from "react-router-dom";
import DatePicker from "../components/common/forms/DatePicker";
import FormButton from "../components/common/forms/FormButton";
import { ExternalLoginProvider } from "../helpers/enums/ExternalLoginProviders";
import useApi from "../hooks/useApi";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import { useEffect } from "react";
import { HTTPStatusCode } from "../helpers/enums/HTTPStatusCode";

const RegisterPage = () => {
  const methods = useForm<UserRegisterRequest>({
    resolver: zodResolver(userRegisterSchema),
  });
  const { register, handleSubmit, formState } = methods;
  const { errors, isSubmitting } = formState;

  const {
    statusCode: registerStatusCode,
    post: registerUser,
    error: registerError,
  } = useApi<[], UserRegisterRequest>(ApiEndpoint.AuthRegister);

  const navigate = useNavigate();

  useEffect(() => {
    if (registerStatusCode === HTTPStatusCode.Ok) {
      navigate("/email-verification");
    }
  }, [registerStatusCode]);

  // Register user in application
  const onSubmit: SubmitHandler<UserRegisterRequest> = async (data) => {
    await registerUser({ body: data });
  };

  return (
    <div className="flex flex-row justify-center items-start gap-20 py-16 w-full">
      <div className="flex flex-col justify-center items-start translate-y-[100px]">
        <h2 className="tracking-wide">
          <span className="text-[#2F2F2F] text-4xl font-semibold">Witaj w </span>
          <span className="text-primaryPurple font-extrabold text-5xl">EventFlow!</span>
        </h2>
        <img
          src={registerHero}
          alt="Obrazek powitalny logowania"
          className="object-contain w-[460px] h-[460px]"
        />
      </div>
      <div className="bg-white min-w-[580px] rounded-md drop-shadow-xl p-10 flex flex-col justify-center items-center gap-10">
        <h3 className="text-black font-bold text-4xl">Zarejestruj się</h3>
        <div className="flex flex-col justify-center items-center gap-6 w-full">
          <FormProvider {...methods}>
            <form
              className="flex flex-col justify-center items-center gap-6 w-full"
              onSubmit={handleSubmit(onSubmit)}
            >
              <Input
                {...register("name")}
                icon={faUserCircle}
                label="Imię"
                type="text"
                name="name"
                error={errors.name}
              />
              <Input
                {...register("surname")}
                icon={faUserCircle}
                label="Nazwisko"
                type="text"
                name="surname"
                error={errors.surname}
              />

              <Input
                {...register("email")}
                icon={faEnvelope}
                label="Email"
                type="email"
                name="email"
                error={errors.email}
              />

              <DatePicker
                {...register("dateOfBirth")}
                label="Data urodzenia"
                name="dateOfBirth"
                error={errors.dateOfBirth}
              />

              <Input
                {...register("password")}
                icon={faKey}
                label="Hasło"
                type="password"
                name="password"
                error={errors.password}
              />

              <Input
                {...register("confirmPassword")}
                icon={faKey}
                label="Powtórz hasło"
                type="password"
                name="confirmPassword"
                error={errors.confirmPassword}
              />
              <FormButton isSubmitting={isSubmitting} text="Zarejestruj się" />
            </form>
          </FormProvider>
          <div className="flex flex-col justify-center items-center gap-6 w-full">
            <div className="flex flex-row justify-between items-center w-full">
              <div className="bg-[#2F2F2F] min-h-[0.5px]" style={{ width: "43%" }}></div>
              <p className="text-[#2F2F2F] text-base">LUB</p>
              <div className="bg-[#2F2F2F] min-h-[0.5px]" style={{ width: "43%" }}></div>
            </div>
            <ExternalLoginButton
              text="Zaloguj się za pomocą konta Google"
              logo={googleLogo}
              loginUrl="/signin-google"
              onClick={() => {
                localStorage.setItem("selectedProvider", ExternalLoginProvider.Google);
              }}
            />
            <ExternalLoginButton
              text="Zaloguj się za pomocą konta Facebook"
              logo={fbLogo}
              loginUrl="/signin-facebook"
              onClick={() => {
                localStorage.setItem("selectedProvider", ExternalLoginProvider.Facebook);
              }}
            />
            <div className="flex flex-row justify-center items-center gap-1">
              <p className="text-base text-[#2f2f2f]">Posiadasz już konto? </p>
              <Link to="/sign-in" className="text-[#6358DC] text-base">
                Zaloguj się
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default RegisterPage;
