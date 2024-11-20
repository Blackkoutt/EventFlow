import { Outlet } from "react-router-dom";
import TopOptions from "../components/topbar/TopOptions";
import AppLogo from "../components/AppLogo";
import Button, { ButtonStyle } from "../components/buttons/Button";
import AppNav from "../components/AppNav";

function AppLayout() {
  return (
    <>
      <header className="flex flex-col justify-center items-center">
        <div className="flex w-[100%] flex-col justify-center items-center gap-4 border-b-4 border-black pt-1 pb-4">
          <div className="self-end">
            <TopOptions />
          </div>
          <div className="w-[100%] flex flex-row justify-between items-center">
            <AppLogo />
            <div className="flex flex-row justify-center items-center gap-[5px]">
              <Button
                text="Zaloguj się"
                width={180}
                height={47}
                style={ButtonStyle.Primary}
                action={() => {}}
              />
              <Button
                text="Zarejestruj się"
                width={180}
                height={47}
                style={ButtonStyle.Secondary}
                action={() => {}}
              />
            </div>
          </div>
        </div>
        <div className="py-[12px]">
          <AppNav />
        </div>
      </header>
      <main className="flex flex-col justify-center items-center">
        <Outlet />
      </main>
      <footer className="flex flex-col justify-center items-center">
        <div className="w-[80%]"></div>
      </footer>
    </>
  );
}
export default AppLayout;
