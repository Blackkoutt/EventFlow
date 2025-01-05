import { forwardRef } from "react";
import { News } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import LabelText from "../../common/LabelText";
import ApiClient from "../../../services/api/ApiClientService";
import DateFormatter from "../../../helpers/DateFormatter";
import { DateFormat } from "../../../helpers/enums/DateFormatEnum";

interface DetailsNewsDialogProps {
  item?: News;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogClose: () => void;
}

const DetailsNewsDialog = forwardRef<HTMLDialogElement, DetailsNewsDialogProps>(
  ({ item, maxWidth, minWidth, onDialogClose, paddingX }: DetailsNewsDialogProps, ref) => {
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
                <h2>Szczegóły news'u</h2>
                <p className="text-textPrimary text-base text-center">
                  Poniżej przedstawiono szczegóły dotyczące wybranego news'u
                </p>
              </div>
              <div className="flex flex-col justify-center items-center gap-2">
                <LabelText labelWidth={122} label="ID:" text={item.id} gap={10} />
                <LabelText labelWidth={122} label="Tytuł:" text={item.title} gap={10} />
                <LabelText
                  labelWidth={122}
                  label="Data publikacji:"
                  text={DateFormatter.FormatDate(item.publicationDate, DateFormat.DateTime)}
                  gap={10}
                />
                <LabelText
                  labelWidth={122}
                  label="Krótki opis:"
                  text={item.shortDescription}
                  gap={10}
                />
                <LabelText
                  labelWidth={122}
                  label="Długi opis:"
                  text={item.longDescription}
                  gap={10}
                />
                <div className="flex flex-row self-start items-center gap-2">
                  <p
                    style={{ fontSize: 16, minWidth: 122 }}
                    className={`font-bold text-end text-textPrimary hover:cursor-default`}
                  >
                    Zdjęcie:
                  </p>
                  <img
                    src={`${ApiClient.GetPhotoEndpoint(item.photoEndpoint)}`}
                    alt={`Zdjęcie newsu o id ${item.id}`}
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

export default DetailsNewsDialog;
