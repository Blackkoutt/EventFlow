import { Outlet, RouterProvider } from "react-router-dom";
import { router } from "../router/Router";
import ToggleButton from "../components/ToggleButton";
import Button, { ButtonStyle } from "../components/Button";
import AppNav from "../components/AppNav";
import TopPageSearchInput from "../components/TopPageSearchInput";

function AppLayout() {
  return (
    <>
      <header>
        <ToggleButton />
        <Button
          text="Zaloguj się"
          paddingX={10}
          paddingY={3}
          style={ButtonStyle.Primary}
          action={() => {}}
        />
        <Button
          text="Zaloguj się"
          paddingX={10}
          paddingY={3}
          style={ButtonStyle.Secondary}
          action={() => {}}
        />
        <Button
          text="Kup teraz"
          paddingX={10}
          paddingY={3}
          style={ButtonStyle.Default}
          action={() => {}}
        />
        <AppNav />
        <TopPageSearchInput />
      </header>
      <main>
        <Outlet />
      </main>
      <footer></footer>
    </>
  );
}
export default AppLayout;
