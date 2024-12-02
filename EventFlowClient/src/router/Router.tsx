import { createBrowserRouter } from "react-router-dom";
import HomePage from "../pages/HomePage";
import NotFoundPage from "../pages/NotFoundPage";
import AboutPage from "../pages/AboutPage";
import ArchivePage from "../pages/ArchivePage";
import EventsPage from "../pages/EventsPage";
import EventPassesPage from "../pages/EventPassesPage";
import FestivalsPage from "../pages/FestivalsPage";
import NewsPage from "../pages/NewsPage";
import RentsPage from "../pages/RentsPage";
import AppLayout from "../layout/AppLayout";
import LoginPage from "../pages/LoginPage";
import RegisterPage from "../pages/RegisterPage";
import ForgotPasswordPage from "../pages/ForgotPasswordPage";
import VerificationEmailSent from "../pages/VerificationEmailSent";
import ProtectedRoute from "../wrappers/ProtectedRoute";
import UserProfile from "../pages/UserProfile";
import Management from "../pages/Management";
import { Roles } from "../helpers/enums/UserRoleEnum";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <AppLayout />,
    errorElement: <NotFoundPage />,
    children: [
      {
        path: "/",
        element: <HomePage />,
      },
      {
        path: "/about",
        element: <AboutPage />,
      },
      {
        path: "/archive",
        element: <ArchivePage />,
      },
      {
        path: "/events",
        element: <EventsPage />,
      },
      {
        path: "/eventpasses",
        element: <EventPassesPage />,
      },
      {
        path: "/festivals",
        element: <FestivalsPage />,
      },
      {
        path: "/news",
        element: <NewsPage />,
      },
      {
        path: "/rents",
        element: <RentsPage />,
      },
      {
        path: "/sign-in",
        element: <LoginPage />,
      },
      {
        path: "/sign-up",
        element: <RegisterPage />,
      },
      {
        path: "/forgot-password",
        element: <ForgotPasswordPage />,
      },
      {
        path: "/email-verification",
        element: <VerificationEmailSent />,
      },
      {
        path: "/profile",
        element: (
          <ProtectedRoute>
            <UserProfile />
          </ProtectedRoute>
        ),
      },
      {
        path: "/management",
        element: (
          <ProtectedRoute allowedRoles={[Roles.Admin]}>
            <Management />
          </ProtectedRoute>
        ),
      },
    ],
  },
]);
