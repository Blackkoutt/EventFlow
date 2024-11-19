import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";

export const ApiModelConfig = {
  [ApiEndpoint.AdditionalServices]: {
    url: "/additionalservices",
  },
  [ApiEndpoint.Equipment]: {
    url: "/equipments",
  },
  /*[ApiEndpoint.Auth]: {
    url: "/auth",
    mappingType: {} as Sponsor,
  },*/
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
