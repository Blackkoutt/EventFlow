import { faPlus, faUpload } from "@fortawesome/free-solid-svg-icons";
import Button, { ButtonStyle } from "../buttons/Button";
import ButtonWithMenu, { ButtonWithMenuElement } from "../buttons/ButtonWithMenu";

interface HeaderTemplateProps {
  headerText: string;
  onCreate: () => void;
  menuElements: ButtonWithMenuElement[];
}

const HeaderTemplate = ({ headerText, onCreate, menuElements }: HeaderTemplateProps) => {
  return (
    <div className="flex flex-row items-center justify-between gap-3">
      <h2 className="text-[#374151]">{headerText}</h2>
      <div className="flex flex-row items-center justify-end gap-3">
        <Button
          text="Dodaj"
          icon={faPlus}
          width={110}
          height={45}
          fontSize={14}
          iconSize={16}
          rounded="rounded-lg"
          isFontSemibold={true}
          style={ButtonStyle.Primary}
          action={onCreate}
        />
        <ButtonWithMenu
          text="Eksport"
          width={120}
          height={20}
          bgColor="#a855f7"
          icon={faUpload}
          menuElements={menuElements}
        />
      </div>
    </div>
  );
};

export default HeaderTemplate;
