import { faPlus, faUpload } from "@fortawesome/free-solid-svg-icons";
import Button, { ButtonStyle } from "../buttons/Button";
import ButtonWithMenu, { ButtonWithMenuElement } from "../buttons/ButtonWithMenu";
import SearchInput from "../common/forms/SearchInput";
import { ChangeEvent } from "react";

interface HeaderTemplateProps {
  headerText: string;
  globalFilterValue: string;
  includeCreate?: boolean;
  onGlobalFilterChange: (e: ChangeEvent<HTMLInputElement>) => void;
  onCreate?: () => void;
  menuElements: ButtonWithMenuElement[];
}

const HeaderTemplate = ({
  headerText,
  onCreate,
  globalFilterValue,
  onGlobalFilterChange,
  includeCreate = true,
  menuElements,
}: HeaderTemplateProps) => {
  return (
    <div className="flex flex-row items-center justify-between gap-3">
      <h2 className="text-[#374151]">{headerText}</h2>
      <div className="flex flex-row items-center justify-end gap-3">
        {includeCreate && (
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
            action={() => onCreate?.()}
          />
        )}
        <ButtonWithMenu
          text="Eksport"
          width={120}
          height={20}
          bgColor="#a855f7"
          icon={faUpload}
          menuElements={menuElements}
        />
        <SearchInput value={globalFilterValue} onChange={(e) => onGlobalFilterChange(e)} />
      </div>
    </div>
  );
};

export default HeaderTemplate;
