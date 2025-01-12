import { forwardRef, useState } from "react";
import Dialog from "../../common/Dialog";
import { Festival } from "../../../models/response_models";
import { EventTabButtonType } from "../../../helpers/enums/EventTabButtonType";
import EventTabButton from "../../buttons/EventTabButton";
import ModifyFestival from "./ModifyFestival";
import FestivalDetails from "./FestivalDetails";
import DeleteFestival from "./DeleteFestival";

interface FestivalClickDialogProps {
  item?: Festival;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const FestivalClickDialog = forwardRef<HTMLDialogElement, FestivalClickDialogProps>(
  (
    {
      item,
      maxWidth,
      minWidth,
      onDialogClose,
      paddingX,
      onDialogSuccess,
    }: FestivalClickDialogProps,
    ref
  ) => {
    const [currentTab, setCurrentTab] = useState<string>(EventTabButtonType.Details);

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
            <EventTabButton
              currentTab={currentTab}
              tabString={EventTabButtonType.Details}
              text="SZCZEGÓŁY"
              onClick={() => {
                setCurrentTab(EventTabButtonType.Details);
              }}
            />
            <EventTabButton
              currentTab={currentTab}
              tabString={EventTabButtonType.Modify}
              text="MODYFIKUJ"
              onClick={() => {
                setCurrentTab(EventTabButtonType.Modify);
              }}
            />
            <EventTabButton
              currentTab={currentTab}
              tabString={EventTabButtonType.Delete}
              text="USUŃ"
              onClick={() => {
                setCurrentTab(EventTabButtonType.Delete);
              }}
            />
          </div>
          <div className="w-full">
            {currentTab === EventTabButtonType.Details && (
              <FestivalDetails maxWidth={1000} item={item} />
            )}
            {currentTab === EventTabButtonType.Modify && (
              <ModifyFestival
                onDialogSuccess={onDialogSuccess}
                onDialogClose={onDialogClose}
                item={item}
              />
            )}
            {currentTab === EventTabButtonType.Delete && (
              <DeleteFestival
                item={item}
                onDialogSuccess={onDialogSuccess}
                onDialogClose={onDialogClose}
              />
            )}
          </div>
        </div>
      </Dialog>
    );
  }
);
export default FestivalClickDialog;
