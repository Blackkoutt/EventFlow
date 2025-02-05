import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";

export const ApiUrlConfig = {
  [ApiEndpoint.AdditionalServices]: {
    url: (id?: number | string) => `/additionalservices${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.AuthValidate]: {
    url: (id?: number | string) => "/auth/validate",
  },
  [ApiEndpoint.AuthRegister]: {
    url: (id?: number | string) => "/auth/register",
  },
  [ApiEndpoint.AuthLogin]: {
    url: (id?: number | string) => "/auth/login",
  },
  [ApiEndpoint.AuthActivate]: {
    url: (id?: number | string) => "/auth/activate",
  },
  [ApiEndpoint.AuthLoginGoogle]: {
    url: (id?: number | string) => "/auth/google-login",
  },
  [ApiEndpoint.AuthLoginFacebook]: {
    url: (id?: number | string) => "/auth/facebook-login",
  },
  [ApiEndpoint.Equipment]: {
    url: (id?: number | string) => `/equipments${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.EventCategory]: {
    url: (id?: number | string) => `/eventcategories${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.EventPass]: {
    url: (id?: number | string) => `/eventpasses${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.EventPassCreateRenewTransaction]: {
    url: (id?: number | string) => `/eventpasses/${id}/create-renew-transaction`,
  },
  [ApiEndpoint.EventPassCreateBuyTransaction]: {
    url: (id?: number | string) => `/eventpasses/create-buy-transaction`,
  },
  [ApiEndpoint.EventPassPDF]: {
    url: (id?: number | string) => `/eventpasses/${id}/pdf`,
  },
  [ApiEndpoint.EventPassJPG]: {
    url: (id?: number | string) => `/eventpasses/${id}/jpg`,
  },
  [ApiEndpoint.EventPassType]: {
    url: (id?: number | string) => `/eventpasstypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Event]: {
    url: (id?: number | string) => `/events${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.EventUpdateHall]: {
    url: (id?: number | string) => `/events/${id}/hall`,
  },
  [ApiEndpoint.EventHallView]: {
    url: (id?: number | string) => `/events/${id}/pdf-hallview`,
  },
  [ApiEndpoint.FAQ]: {
    url: (id?: number | string) => `/faq${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Festival]: {
    url: (id?: number | string) => `/festivals${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.HallRent]: {
    url: (id?: number | string) => `/hallrents${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.HallRentCreateTransaction]: {
    url: (id?: number | string) => `/hallrents/create-rent-transaction`,
  },
  [ApiEndpoint.HallRentUpdateHall]: {
    url: (id?: number | string) => `/hallrents/${id}/hall`,
  },
  [ApiEndpoint.HallRentHallView]: {
    url: (id?: number | string) => `/hallrents/${id}/pdf-hallview`,
  },
  [ApiEndpoint.HallRentPDFInovice]: {
    url: (id?: number | string) => `/hallrents/${id}/pdf-invoice`,
  },
  [ApiEndpoint.Hall]: {
    url: (id?: number | string) => `/halls${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.HallView]: {
    url: (id?: number | string) => `/halls/${id}/pdf-hallview`,
  },
  [ApiEndpoint.HallType]: {
    url: (id?: number | string) => `/halltypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.MediaPatron]: {
    url: (id?: number | string) => `/mediapatrons${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.News]: {
    url: (id?: number | string) => `/news${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Organizer]: {
    url: (id?: number | string) => `/organizers${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Partner]: {
    url: (id?: number | string) => `/partners${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.PaymentType]: {
    url: (id?: number | string) => `/paymenttypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Reservation]: {
    url: (id?: number | string) => `/reservations${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.ReservationZIPTickets]: {
    url: (id?: number | string) => `/reservations/${id}/jpg-tickets`,
  },
  [ApiEndpoint.ReservationPDFTicket]: {
    url: (id?: number | string) => `/reservations/${id}/pdf-ticket`,
  },
  [ApiEndpoint.SeatType]: {
    url: (id?: number | string) => `/seattypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Sponsor]: {
    url: (id?: number | string) => `/sponsors${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.Stats]: {
    url: (id?: number | string) => "/statistics",
  },
  [ApiEndpoint.StatsPDF]: {
    url: (id?: number | string) => "/statistics/pdf-report",
  },
  [ApiEndpoint.TicketType]: {
    url: (id?: number | string) => `/tickettypes${id ? `/${id}` : ""}`,
  },
  [ApiEndpoint.UserInfo]: {
    url: (id?: number | string) => "/users/info",
  },
  [ApiEndpoint.User]: {
    url: (id?: number | string) => `/users${id ? `/${id}` : ""}`,
  },
};
