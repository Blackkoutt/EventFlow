import { Outlet } from "react-router-dom";
import TabButton from "../components/buttons/TabButton";
import { Carousel } from "primereact/carousel";
import { useState } from "react";

type TabButtonProps = {
  width: number;
  text: string;
  path: string;
};

const Management = () => {
  const [tabButtons] = useState<TabButtonProps[]>([
    { width: 200, text: "Strona główna", path: "/management" },
    { width: 200, text: "Dodatkowe usługi", path: "/management/additional-services" },
    { width: 200, text: "Wyposażenie sal", path: "/management/hall-equipments" },
    { width: 200, text: "Kategorie wydarzeń", path: "/management/event-categories" },
    { width: 200, text: "Karnety", path: "/management/event-passes" },
    { width: 200, text: "Typy karnetów", path: "/management/event-pass-types" },
    { width: 200, text: "Wydarzenia", path: "/management/events" },
    { width: 200, text: "Festiwale", path: "/management/festivals" },
    { width: 200, text: "FAQ", path: "/management/faq" },
    { width: 200, text: "Wynajmy sal", path: "/management/hall-rents" },
    { width: 200, text: "Sale", path: "/management/halls" },
    { width: 200, text: "Typy sal", path: "/management/hall-types" },
    { width: 200, text: "Patroni medialni", path: "/management/media-patrons" },
    { width: 200, text: "Newsy", path: "/management/news" },
    { width: 200, text: "Organizatorzy", path: "/management/organizators" },
    { width: 200, text: "Partnerzy", path: "/management/partners" },
    { width: 200, text: "Typy Płatności", path: "/management/payment-types" },
    { width: 200, text: "Rezerwacje", path: "/management/reservations" },
    { width: 200, text: "Typy miejsc", path: "/management/seat-types" },
    { width: 200, text: "Sponsorzy", path: "/management/sponsors" },
    { width: 200, text: "Statystyki", path: "/management/stats" },
    { width: 200, text: "Typy biletów", path: "/management/ticket-types" },
    { width: 200, text: "Użytkownicy", path: "/management/users" },
  ]);

  const responsiveOptions = [
    {
      breakpoint: "1400px",
      numVisible: 4,
      numScroll: 1,
    },
    {
      breakpoint: "1150px",
      numVisible: 3,
      numScroll: 1,
    },
    {
      breakpoint: "900px",
      numVisible: 2,
      numScroll: 1,
    },
    {
      breakpoint: "650px",
      numVisible: 1,
      numScroll: 1,
    },
  ];

  const productTemplate = (tabButtonProps: TabButtonProps) => {
    return (
      <TabButton
        width={tabButtonProps.width}
        text={tabButtonProps.text}
        path={tabButtonProps.path}
      />
    );
  };

  return (
    <div className="flex flex-col w-full gap-8">
      <div className="mt-10">
        <Carousel
          value={tabButtons}
          showIndicators={false}
          numScroll={1}
          numVisible={5}
          style={{ zIndex: 10 }}
          responsiveOptions={responsiveOptions}
          itemTemplate={productTemplate}
        />
      </div>
      <div className="flex mb-12 py-11 flex-col items-center justify-start rounded-lg border-gray-100 border-[2px] bg-white shadow-xl p-3 w-full min-h-[660px]">
        <Outlet />
      </div>
    </div>
  );
};
export default Management;
