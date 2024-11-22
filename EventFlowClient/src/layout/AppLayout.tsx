import { Link, Outlet } from "react-router-dom";
import TopOptions from "../components/topbar/TopOptions";
import AppLogo from "../components/AppLogo";
import Button, { ButtonStyle } from "../components/buttons/Button";
import AppNav from "../components/layout/AppNav";
import { faLocationDot, faEnvelope } from "@fortawesome/free-solid-svg-icons";
import SocialMediaIcon from "../components/common/SocialMediaIcon";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import ContactItem from "../components/layout/ContactItem";

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
                height={57}
                fontSize={17}
                style={ButtonStyle.Primary}
                action={() => {}}
              />
              <Button
                text="Zarejestruj się"
                width={180}
                height={57}
                fontSize={17}
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
      <footer className="w-full border-t-4 border-black pt-6 pb-10 flex flex-col gap-8 items-start justify-start">
        <div className="flex flex-row justify-between items-center w-full pr-8">
          <AppLogo width={360} height={80} />
          <div className="flex flex-row items-center justify-start gap-11">
            <SocialMediaIcon
              icon="fa-facebook-f"
              linkTo="https://www.facebook.com/?locale=pl_PL"
              title="Fanpage EventFlow na Facebook'u!"
              iconSize={48}
              center={false}
              width={65}
              height={65}
              hoverColor="#0866ff"
            />
            <SocialMediaIcon
              icon="fa-youtube"
              linkTo="https://www.youtube.com/"
              title="Kanał EventFlow na YouTube!"
              iconSize={34}
              width={65}
              height={65}
              hoverColor="#ff0033"
            />
            <SocialMediaIcon
              linkTo="https://www.instagram.com/"
              title="Profil EventFlow na Instragramie!"
              icon="fa-instagram"
              iconSize={40}
              width={65}
              height={65}
              hoverColor="linear-gradient(45deg, #F58529, #DD2A7B, #8134AF, #515BD4)"
            />
          </div>
        </div>
        <div className="w-full flex flex-row justify-between items-center">
          <div className="flex flex-col justify-start items-center gap-8">
            <ContactItem
              icon={faLocationDot}
              header="EventFlow"
              text="ul. Jana Kowalskiego 12/34 <br />
                    00-001 Warszawa"
            />
            <ContactItem
              icon={faEnvelope}
              header="Kontakt"
              text="tel: 123 456 789<br />
                    e-mail: eventflow@event.com"
            />
          </div>
          <div className="flex flex-col justify-start items-start gap-8">
            <div className="flex flex-col justify-start items-start gap-2">
              <h4 className="text-[18px] font-semibold text-black border-b-4 border-b-primaryPurple">
                MENU
              </h4>
              <AppNav fontSize={18} isSemibold={false} textColor="#000" gap={35} />
            </div>
            <div className="flex flex-row justify-between w-full items-end">
              <div className="flex flex-col justify-start items-start gap-2">
                <h4 className="text-[18px] font-semibold text-black border-b-4 border-b-primaryPurple">
                  ZOBACZ RÓWNIEŻ
                </h4>
                <div className="flex flex-row justify-start items-start gap-14">
                  <div className="flex flex-col justify-start items-start gap-4">
                    <Link to="/" className="text-[18px] text-black font-normal">
                      Deklaracja dostępności
                    </Link>
                    <Link to="/" className="text-[18px] text-black font-normal">
                      Polityka prywatności
                    </Link>
                  </div>
                  <div className="flex flex-col justify-start items-start gap-4">
                    <Link to="/" className="text-[18px] text-black font-normal">
                      RODO
                    </Link>
                    <Link to="/" className="text-[18px] text-black font-normal">
                      Regulamin
                    </Link>
                  </div>
                </div>
              </div>
              <p className="text-black">2024 &copy; EventFlow</p>
            </div>
          </div>
        </div>
      </footer>
    </>
  );
}
export default AppLayout;
