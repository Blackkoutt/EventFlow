import { forwardRef, SyntheticEvent, useEffect, useRef, useState } from "react";
import { Hall, HallRent, Seat, SeatType } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import Button, { ButtonStyle } from "../../buttons/Button";
import { faCouch, faDownload, faPenToSquare, faXmark } from "@fortawesome/free-solid-svg-icons";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import ContextMenu from "../../common/ContextMenu";

interface ModifyHallDialogProps {
  hallId?: number;
  onDialogClose: () => void;
}

interface ContextMenuState {
  selectedSeatNr?: number;
  position: { elementNr: number; y: number };
  items: { label: string; action: (selectedSeatNr?: number) => void }[];
}

const ModifyHallDialog = forwardRef<HTMLDialogElement, ModifyHallDialogProps>(
  ({ hallId, onDialogClose }: ModifyHallDialogProps, ref) => {
    const { data: hallWithDetails, get: getHallWithDetails } = useApi<Hall>(ApiEndpoint.Hall);
    const { data: seatTypes, get: getSeatTypes } = useApi<SeatType>(ApiEndpoint.SeatType);
    //const [selectedSeatNr, setSelectedSeatNr] = useState<number | undefined>(undefined);
    const [hallSeats, setHallSeats] = useState<Seat[]>([]);
    const [contextMenu, setContextMenu] = useState<ContextMenuState | null>(null);

    useEffect(() => {
      if (hallId !== undefined) {
        getHallWithDetails({ id: hallId, queryParams: undefined });
        getSeatTypes({ id: undefined, queryParams: undefined });
      }
    }, [hallId]);

    useEffect(() => {
      console.log(hallWithDetails);
      if (hallWithDetails !== undefined && hallWithDetails.length !== 0) {
        setHallSeats(hallWithDetails[0].seats);
      }
    }, [hallWithDetails]);

    const changeSeatType = (seatNumber: number, seatType?: SeatType) => {
      console.log("here");
      setHallSeats((prevSeats) => {
        // Seat not exist or exisst, seatType is undefined
        if (seatType === undefined) {
          return prevSeats.filter((seat) => seat.seatNr !== seatNumber);
        }

        // Seat exist, seatType is defined
        const seatExists = prevSeats.some((seat) => seat.seatNr === seatNumber);
        if (seatExists) {
          return prevSeats.map((seat) => {
            if (seat.seatNr === seatNumber) {
              return {
                ...seat,
                seatType: seatType,
              };
            }
            return seat;
          });
        }

        // Seat not exist, seatType is defined
        return [
          ...prevSeats,
          {
            seatNr: seatNumber,
            row: Math.ceil(seatNumber / 10),
            gridRow: Math.ceil(seatNumber / 10),
            column: ((seatNumber - 1) % 10) + 1,
            isAvailable: true,
            gridColumn: ((seatNumber - 1) % 10) + 1,
            seatType: seatType,
          } as Seat,
        ];
      });
    };

    const generateContextMenuItems = () => {
      if (!seatTypes) return [];

      const items = seatTypes.map((seatType) => ({
        label: seatType.name,
        action: (selectedSeatNr?: number) => {
          if (selectedSeatNr !== undefined) changeSeatType(selectedSeatNr, seatType);
        },
      }));

      items.push({
        label: "Miejsce nieaktywne",
        action: (selectedSeatNr?: number) => {
          if (selectedSeatNr !== undefined) changeSeatType(selectedSeatNr, undefined);
        },
      });

      return items;
    };

    const getSeats = (maxNumberOfSeatsRows?: number, maxNumberOfSeatsColumns?: number) => {
      if (maxNumberOfSeatsRows !== undefined && maxNumberOfSeatsColumns !== undefined) {
        return (
          <div className="flex flex-col gap-[10px]" onContextMenu={(e) => e.preventDefault()}>
            {Array.from({ length: maxNumberOfSeatsRows }, (_, rowIndex) => (
              <div key={rowIndex} className="grid grid-cols-10 gap-[10px]">
                {Array.from({ length: maxNumberOfSeatsColumns }, (_, colIndex) => {
                  const seatNumber = rowIndex * maxNumberOfSeatsColumns + colIndex + 1;
                  const seat = hallSeats.find((s) => s.seatNr == seatNumber);
                  //const isAvailable = seatNumbers.includes(seatNumber);

                  return (
                    <div
                      key={`${rowIndex}-${colIndex}`}
                      className={`border px-[10px] py-[6px] text-center text-[12px] flex flex-col justify-center gap-1 items-center ${
                        seat
                          ? "border-gray-300 text-white hover:cursor-pointer"
                          : "border-gray-400 bg-gray-200 text-gray-500"
                      }`}
                      // onContextMenu={(event) => onRightClick(event, seatNumber)}
                      onContextMenu={(e) => handleContextMenu(e, seatNumber)}
                      style={{ backgroundColor: seat?.seatType?.seatColor }}
                    >
                      <FontAwesomeIcon icon={faCouch} fontSize={13} />
                      <p className="hover:cursor-default">{seatNumber}</p>
                    </div>
                  );
                })}
              </div>
            ))}
          </div>
        );
      }
      // return "";
    };

    const handleContextMenu = (event: React.MouseEvent, seatNumber: number) => {
      event.preventDefault();
      //setSelectedSeatNr(seatNumber);
      setContextMenu({
        selectedSeatNr: seatNumber,
        position: { elementNr: seatNumber, y: event.pageY },
        items: generateContextMenuItems(),
      });
    };

    const handleCloseContextMenu = () => {
      setContextMenu(null);
    };

    return (
      <div>
        {hallId !== undefined && hallWithDetails[0]?.hallDetails !== undefined && (
          <Dialog ref={ref}>
            <div className="flex flex-col justify-center items-center px-5 pb-2 gap-6">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Modyfikacja sali</h2>
                <p className="text-textPrimary text-base text-center">
                  Zmodyfikuj salę zmieniając ustawienie lub typ miejsc
                </p>
              </div>
              <div className="flex flex-col justify-center items-center gap-3">
                <div></div>
                <div className="flex flex-col gap-2">
                  {getSeats(
                    hallWithDetails[0].hallDetails?.maxNumberOfSeatsRows,
                    hallWithDetails[0].hallDetails?.maxNumberOfSeatsColumns
                  )}
                  {contextMenu && (
                    <ContextMenu
                      selectedSeatNr={contextMenu.selectedSeatNr}
                      items={contextMenu.items}
                      position={contextMenu.position}
                      onClose={handleCloseContextMenu}
                    />
                  )}
                </div>
              </div>
              <div className="flex flex-row justify-center items-center gap-2">
                <Button
                  text="Anuluj"
                  width={145}
                  height={45}
                  icon={faXmark}
                  iconSize={18}
                  style={ButtonStyle.DefaultGray}
                  action={onDialogClose}
                ></Button>
                <Button
                  text="Modyfikuj"
                  width={145}
                  height={45}
                  icon={faPenToSquare}
                  iconSize={18}
                  style={ButtonStyle.Primary}
                  action={() => {}}
                ></Button>
              </div>
            </div>
          </Dialog>
        )}
      </div>
    );
  }
);

export default ModifyHallDialog;
