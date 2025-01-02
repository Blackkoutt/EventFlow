import { forwardRef } from "react";
import { AdditionalServices, Equipment } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import LabelText from "../../common/LabelText";

interface DetailsEquipmentDialogProps {
  item?: Equipment;
  onDialogClose: () => void;
}

const DetailsEquipmentDialog = forwardRef<HTMLDialogElement, DetailsEquipmentDialogProps>(
  ({ item, onDialogClose }: DetailsEquipmentDialogProps, ref) => {
    return (
      <div>
        {item && (
          <Dialog ref={ref} onClose={onDialogClose}>
            <article className="flex flex-col justify-center items-center px-5 pb-2 gap-5 max-w-[750px]">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Szczegóły wyposażenia sali</h2>
                <p className="text-textPrimary text-base text-center">
                  Poniżej przedstawiono szczegóły dotyczące wybranego wyposażenia
                </p>
              </div>
              <div className="flex flex-col justify-center items-center gap-2">
                <LabelText labelWidth={60} label="ID:" text={item.id} gap={10} />
                <LabelText labelWidth={60} label="Nazwa:" text={item.name} gap={10} />
                <LabelText labelWidth={60} label="Opis:" text={item.description} gap={10} />
              </div>
            </article>
          </Dialog>
        )}
      </div>
    );
  }
);

export default DetailsEquipmentDialog;
