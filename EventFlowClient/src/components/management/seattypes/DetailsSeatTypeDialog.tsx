import { forwardRef } from "react";
import { SeatType } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import LabelText from "../../common/LabelText";

interface DetailsSeatTypeDialogProps {
  item?: SeatType;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogClose: () => void;
}

const DetailsSeatTypeDialog = forwardRef<HTMLDialogElement, DetailsSeatTypeDialogProps>(
  ({ item, maxWidth, minWidth, onDialogClose, paddingX }: DetailsSeatTypeDialogProps, ref) => {
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
              <div className="flex flex-col justify-center items-center gap-2 -translate-x-[45px]">
                <LabelText labelWidth={214} label="ID:" text={item.id} gap={10} />
                <LabelText labelWidth={214} label="Nazwa:" text={item.name} gap={10} />
                <LabelText labelWidth={214} label="Opis:" text={item.description} gap={10} />
                <div className="flex flex-row justify-start self-start items-start gap-3">
                  <p
                    style={{ fontSize: 16, minWidth: 214 }}
                    className="font-bold text-end text-textPrimary hover:cursor-default"
                  >
                    Kolor:
                  </p>
                  <div
                    className="w-[20px] h-[20px]"
                    title={item.seatColor}
                    style={{ backgroundColor: item.seatColor }}
                  ></div>
                </div>
                <LabelText
                  labelWidth={214}
                  label="Procent dodatkowej opłaty:"
                  text={`${item.addtionalPaymentPercentage} %`}
                  gap={10}
                />
              </div>
            </article>
          </Dialog>
        )}
      </div>
    );
  }
);

export default DetailsSeatTypeDialog;
