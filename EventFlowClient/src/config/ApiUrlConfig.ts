import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";

export const ApiUrlConfig = {
  [ApiEndpoint.AdditionalServices]: {
    url: (id?: number) => `/additionalservices${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.AuthValidate]: {
    url: (id?: number) => "/auth/validate",
  },
  [ApiEndpoint.AuthRegister]: {
    url: (id?: number) => "/auth/register",
  },
  [ApiEndpoint.AuthLogin]: {
    url: (id?: number) => "/auth/login",
  },
  [ApiEndpoint.AuthActivate]: {
    url: (id?: number) => "/auth/activate",
  },
  [ApiEndpoint.AuthLoginGoogle]: {
    url: (id?: number) => "/auth/google-login",
  },
  [ApiEndpoint.AuthLoginFacebook]: {
    url: (id?: number) => "/auth/facebook-login",
  },
  [ApiEndpoint.Equipment]: {
    url: (id?: number) => `/equipments${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.EventCategory]: {
    url: (id?: number) => `/eventcategories${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.EventPass]: {
    url: (id?: number) => `/eventpasses${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.EventPassType]: {
    url: (id?: number) => `/eventpasstypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Event]: {
    url: (id?: number) => `/events${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.FAQ]: {
    url: (id?: number) => `/faq${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Festival]: {
    url: (id?: number) => `/festivals${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.HallRent]: {
    url: (id?: number) => `/hallrents${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Hall]: {
    url: (id?: number) => `/halls${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.HallType]: {
    url: (id?: number) => `/halltypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.MediaPatron]: {
    url: (id?: number) => `/mediapatrons${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.News]: {
    url: (id?: number) => `/news${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Organizer]: {
    url: (id?: number) => `/organizers${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Partner]: {
    url: (id?: number) => `/partners${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.PaymentType]: {
    url: (id?: number) => `/paymenttypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Reservation]: {
    url: (id?: number) => `/reservations${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.ReservationZIPTickets]: {
    url: (id?: number) => `/reservations/${id}/jpg-tickets`,
  },
  [ApiEndpoint.ReservationPDFTicket]: {
    url: (id?: number) => `/reservations/${id}/pdf-ticket`,
  },
  [ApiEndpoint.SeatType]: {
    url: (id?: number) => `/seattypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Sponsor]: {
    url: (id?: number) => `/sponsors${id ? `/${id}` : ""}`,
  },
  /*[ApiEndpoint.Statistics]: {
    url: "/statistics",
    mappingType: {} as Sponsor,
  },*/
  [ApiEndpoint.TicketType]: {
    url: (id?: number) => `/tickettypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.UserInfo]: {
    url: (id?: number) => "/users/info",
  },
};
