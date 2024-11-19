import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import AppLayout from "./layout/AppLayout.tsx";
import { router } from "./router/Router.tsx";
import { RouterProvider } from "react-router-dom";

const originalWarn = console.warn;
console.warn = (...args) => {
  if (args[0].includes("React Router Future Flag Warning")) {
    return;
  }
  originalWarn(...args);
};

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
