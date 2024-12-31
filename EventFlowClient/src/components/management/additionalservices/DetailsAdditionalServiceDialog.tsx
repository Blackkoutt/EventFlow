import { forwardRef } from "react";
import { AdditionalServices } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import LabelText from "../../common/LabelText";

interface DetailsAdditonalServiceDialogProps {
  additionalService?: AdditionalServices;
}

const DetailsAdditionalServiceDialog = forwardRef<
  HTMLDialogElement,
  DetailsAdditonalServiceDialogProps
>(({ additionalService }: DetailsAdditonalServiceDialogProps, ref) => {
  return (
    <div>
      {additionalService && (
        <Dialog ref={ref}>
          <article className="flex flex-col justify-center items-center px-5 pb-2 gap-5 max-w-[750px]">
            <div className="flex flex-col justify-center items-center gap-2">
              <h2>Szczegóły dodatkowej usługi</h2>
              <p className="text-textPrimary text-base text-center">
                Poniżej przedstawiono szczegóły dotyczące wybranej usługi
              </p>
            </div>
            <div className="flex flex-col justify-center items-center gap-2">
              <LabelText labelWidth={60} label="ID:" text={additionalService.id} gap={10} />
              <LabelText labelWidth={60} label="Nazwa:" title={additionalService.name} gap={10} />
              <LabelText
                labelWidth={60}
                label="Cena:"
                text={`${additionalService.price} zł`}
                gap={10}
              />
              <LabelText
                labelWidth={60}
                label="Opis:"
                text={additionalService.description}
                gap={10}
              />
            </div>
          </article>
        </Dialog>
      )}
    </div>
  );
});

export default DetailsAdditionalServiceDialog;
