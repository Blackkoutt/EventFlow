import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { router } from "./router/Router.tsx";
import { RouterProvider } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext.tsx";
import { PrimeReactProvider } from "primereact/api";
//import "primeflex/primeflex.css";
//import "primeicons/primeicons.css";
import "primereact/resources/primereact.min.css";
import "primereact/resources/themes/lara-light-blue/theme.css";
import { Slide, ToastContainer } from "react-toastify";

const originalWarn = console.warn;
console.warn = (...args) => {
  if (args[0].includes("React Router Future Flag Warning")) {
    return;
  }
  originalWarn(...args);
};

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <PrimeReactProvider value={{ unstyled: false }}>
      <AuthProvider>
        <RouterProvider router={router} />
        <ToastContainer
          position="bottom-right"
          autoClose={4000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
          theme="colored"
          transition={Slide}
          limit={5}
          style={{ zIndex: "9999 !important" }}
        />
      </AuthProvider>
    </PrimeReactProvider>
  </StrictMode>
);
