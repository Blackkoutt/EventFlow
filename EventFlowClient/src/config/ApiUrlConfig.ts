import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";

export const ApiUrlConfig = {
  [ApiEndpoint.AdditionalServices]: {
    url: "/additionalservices",
  },
  [ApiEndpoint.AuthValidate]: {
    url: "/auth/validate",
  },
  [ApiEndpoint.AuthRegister]: {
    url: "/auth/register",
  },
  [ApiEndpoint.AuthLogin]: {
    url: "/auth/login",
  },
  [ApiEndpoint.AuthLoginGoogle]: {
    url: "/auth/google-login",
  },
  [ApiEndpoint.AuthLoginFacebook]: {
    url: "/auth/facebook-login",
  },
  [ApiEndpoint.Equipment]: {
    url: "/equipments",
  },
  [ApiEndpoint.EventCategory]: {
    url: "/eventcategories",
  },
  [ApiEndpoint.EventPass]: {
    url: "/eventpasses",
  },
  [ApiEndpoint.EventPassType]: {
    url: "/eventpasstypes",
  },
  [ApiEndpoint.Event]: {
    url: "/events",
  },
  [ApiEndpoint.FAQ]: {
    url: "/faq",
  },
  [ApiEndpoint.Festival]: {
    url: "/festivals",
  },
  [ApiEndpoint.HallRent]: {
    url: "/hallrents",
  },
  [ApiEndpoint.Hall]: {
    url: "/halls",
  },
  [ApiEndpoint.HallType]: {
    url: "/halltypes",
  },
  [ApiEndpoint.MediaPatron]: {
    url: "/mediapatrons",
  },
  [ApiEndpoint.News]: {
    url: "/news",
  },
  [ApiEndpoint.Organizer]: {
    url: "/organizers",
  },
  [ApiEndpoint.Partner]: {
    url: "/partners",
  },
  [ApiEndpoint.PaymentType]: {
    url: "/paymenttypes",
  },
  [ApiEndpoint.Reservation]: {
    url: "/reservations",
  },
  [ApiEndpoint.SeatType]: {
    url: "/seattypes",
  },
  [ApiEndpoint.Sponsor]: {
    url: "/sponsors",
  },
  /*[ApiEndpoint.Statistics]: {
    url: "/statistics",
    mappingType: {} as Sponsor,
  },*/
  [ApiEndpoint.TicketType]: {
    url: "/tickettypes",
  },
};
