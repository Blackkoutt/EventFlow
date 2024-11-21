import { Outlet } from "react-router-dom";
import TopOptions from "../components/topbar/TopOptions";
import AppLogo from "../components/AppLogo";
import Button, { ButtonStyle } from "../components/buttons/Button";
import AppNav from "../components/AppNav";
import SocialMediaIcon from "../components/common/SocialMediaIcon";

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
        <div className="w-[80%] border-t-4 border-black pt-5 flex flex-col gap-8 items-start justify-start">
          <div className="flex flex-row justify-between items-start w-full pr-8">
            <AppLogo width={316} height={73} />
            <div className="flex flex-row items-center justify-start gap-11">
              <SocialMediaIcon
                icon="fa-facebook-f"
                iconSize={48}
                center={false}
                width={65}
                height={65}
              />
              <SocialMediaIcon icon="fa-youtube" iconSize={34} width={65} height={65} />
              <SocialMediaIcon icon="fa-instagram" iconSize={40} width={65} height={65} />
            </div>
          </div>
        </div>
      </footer>
    </>
  );
}
export default AppLayout;
