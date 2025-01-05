import { forwardRef } from "react";
import { HallType } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import LabelText from "../../common/LabelText";
import ApiClient from "../../../services/api/ApiClientService";

interface DetailsHallTypeDialogProps {
  item?: HallType;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogClose: () => void;
}

const DetailsHallTypeDialog = forwardRef<HTMLDialogElement, DetailsHallTypeDialogProps>(
  ({ item, maxWidth, minWidth, onDialogClose, paddingX }: DetailsHallTypeDialogProps, ref) => {
    return (
      <div>
        {item && (
          <Dialog
            ref={ref}
            maxWidth={maxWidth}
            paddingLeft={paddingX}
            paddingRight={paddingX}
            minWidth={minWidth}
            onClose={onDialogClose}
          >
            <article className="flex flex-col justify-center items-center px-5 pb-2 gap-5 max-w-[750px]">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Szczegóły typu sali</h2>
                <p className="text-textPrimary text-base text-center">
                  Poniżej przedstawiono szczegóły dotyczące wybranego typu sali
                </p>
              </div>
              <div className="flex flex-col justify-center items-center gap-2">
                <LabelText labelWidth={133} label="ID:" text={item.id} gap={10} />
                <LabelText labelWidth={133} label="Nazwa:" text={item.name} gap={10} />
                <LabelText labelWidth={133} label="Opis:" text={item.description} gap={10} />
                <LabelText
                  labelWidth={133}
                  label="Wyposażenie:"
                  text={item.equipments.map((eq) => eq.name).join(", ")}
                  gap={10}
                />
                <div className="flex flex-row self-start items-center gap-2">
                  <p
                    style={{ fontSize: 16, width: 133 }}
                    className={`font-bold text-end text-textPrimary hover:cursor-default`}
                  >
                    Zdjęcie:
                  </p>
                  <img
                    src={`${ApiClient.GetPhotoEndpoint(item.photoEndpoint)}`}
                    alt={`Zdjęcie typu sali o id ${item.id}`}
                    className="w-[100px] h-[100px] object-contain"
                  />
                </div>
              </div>
            </article>
          </Dialog>
        )}
      </div>
    );
  }
);

export default DetailsHallTypeDialog;
