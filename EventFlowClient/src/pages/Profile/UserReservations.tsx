import { useEffect, useState } from "react";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { Reservation, Seat } from "../../models/response_models";
import { DataTable } from "primereact/datatable";
import { Column, ColumnBodyOptions } from "primereact/column";
import DateFormatter from "../../helpers/DateFormatter";
import { DateFormat } from "../../helpers/enums/DateFormatEnum";

const UserReservations = () => {
  const { data: reservations, get: getReservations } = useApi<Reservation>(ApiEndpoint.Reservation);
  useEffect(() => {
    getReservations({ id: undefined, queryParams: undefined });
  }, []);

  useEffect(() => {
    console.log(reservations);
  }, [reservations]);

  const actionsTemplate = (rowData: Reservation, options: ColumnBodyOptions) => {
    return (
      <div className="flex flex-row justify-center items-center">
        <button>Szczegóły</button>
        <button>Pobierz bilet</button>
        <button>Anuluj</button>
      </div>
    );
  };

  return (
    <div className="max-w-[950px] self-center">
      <DataTable
        value={reservations}
        paginator
        removableSort
        rows={5}
        rowsPerPageOptions={[5, 10, 25, 50]}
        stripedRows
        showGridlines
        tableStyle={{ minWidth: "50rem" }}
      >
        <Column field="id" sortable header="ID"></Column>
        <Column
          field="reservationDate"
          sortable
          header="Data rezerwacji"
          body={(rowData) => DateFormatter.FormatDate(rowData.reservationDate, DateFormat.DateTime)}
        ></Column>
        <Column field="ticket.event.name" sortable header="Wydarzenie" />
        <Column field="ticket.festival?.name" sortable header="Festiwal" />

        <Column field="ticket.event.hall.hallNr" sortable header="Nr sali" />
        <Column
          field="seats"
          sortable
          header="Nr Miejsc"
          body={(rowData) => rowData.seats.map((seat: Seat) => seat.seatNr).join(", ")}
        />
        <Column field="paymentAmount" sortable header="Koszt (zł)"></Column>
        <Column field="reservationStatus" sortable header="Status"></Column>
        <Column header="Akcja" body={actionsTemplate}></Column>
        {/* <Column
        sortable
        field="paymentDate"
        header="Data płatności"
        body={(rowData) => DateFormatter.FormatDate(rowData.paymentDate, DateFormat.DateTime)}
      ></Column> */}
      </DataTable>
    </div>
  );
};
export default UserReservations;
