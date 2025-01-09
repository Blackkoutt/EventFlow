import { forwardRef, useState } from "react";
import Dialog from "../../common/Dialog";
import LabelText from "../../common/LabelText";
import { EventEntity } from "../../../models/response_models";
import EventDetails from "./EventDetails";

interface EventClickDialogProps {
  item?: EventEntity;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

enum TabButtonType {
  Details = "Details",
  Modify = "Modify",
  Delete = "Delete",
}

const EventClickDialog = forwardRef<HTMLDialogElement, EventClickDialogProps>(
  (
    { item, maxWidth, minWidth, onDialogClose, paddingX, onDialogSuccess }: EventClickDialogProps,
    ref
  ) => {
    const [currentTab, setCurrentTab] = useState<string>(TabButtonType.Details);

    return (
      <Dialog
        ref={ref}
        maxWidth={maxWidth}
        paddingLeft={paddingX}
        paddingRight={paddingX}
        minWidth={minWidth}
        onClose={onDialogClose}
      >
        <div className="flex flex-col justify-center items-center w-full gap-5">
          <div className="flex flex-row justify-end items-center gap-4 w-full mt-2">
            <button
              type="button"
              className={`hover:cursor-pointer ${
                currentTab === TabButtonType.Details
                  ? "border-b-primaryPurple font-semibold border-b-2 text-primaryPurple"
                  : "text-textPrimary"
              }`}
              onClick={() => {
                setCurrentTab(TabButtonType.Details);
              }}
            >
              SZCZEGÓŁY
            </button>
            <button
              type="button"
              className={`hover:cursor-pointer ${
                currentTab === TabButtonType.Modify
                  ? "border-b-primaryPurple font-semibold border-b-2 text-primaryPurple"
                  : "text-textPrimary"
              }`}
              onClick={() => {
                setCurrentTab(TabButtonType.Modify);
              }}
            >
              MODYFIKUJ
            </button>
            <button
              type="button"
              className={`hover:cursor-pointer ${
                currentTab === TabButtonType.Delete
                  ? "border-b-primaryPurple font-semibold border-b-2 text-primaryPurple"
                  : "text-textPrimary"
              }`}
              onClick={() => {
                setCurrentTab(TabButtonType.Delete);
              }}
            >
              USUŃ
            </button>
          </div>
          <div className="w-full">
            {currentTab === TabButtonType.Details && <EventDetails item={item} />}
            {currentTab === TabButtonType.Modify && <div>Modifykacja</div>}
            {currentTab === TabButtonType.Delete && <div>Usuwanie</div>}
          </div>
        </div>
      </Dialog>
    );
  }
);
export default EventClickDialog;
