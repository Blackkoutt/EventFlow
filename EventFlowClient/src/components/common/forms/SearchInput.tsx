import { ChangeEvent, useState } from "react";
import magnifying_glass from "../../../assets/magnifying_glass.png";

interface SearchInputProps {
  width?: number;
  value: string;
  onChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

const SearchInput = ({ width = 180, value, onChange }: SearchInputProps) => {
  const [isFocused, setIsFocused] = useState(false);
  return (
    <>
      <div
        className={`bg-[#ECECEC] rounded-md px-3 py-3
            cursor-text
            relative inline-flex items-center ${
              isFocused
                ? "outline outline-primaryPurple outline-2"
                : "outline outline-1 outline-[#d2d2d2]"
            }`}
        style={{ width: width }}
        tabIndex={1}
        onFocus={() => setIsFocused(true)}
        onBlur={() => setIsFocused(false)}
      >
        <input
          type="text"
          placeholder="Szukaj..."
          style={{ outline: "none" }}
          value={value}
          onChange={(e) => onChange(e)}
          className="w-full h-full text-[#2F2F2F] font-semibold bg-[#ECECEC]"
        />
        <button className="absolute top-[30%] right-[10px] bg-transparent px-0 py-0">
          <img src={magnifying_glass} alt="Ikona wyszukiwania" className="w-[18px] h-[18px]" />
        </button>
      </div>
    </>
  );
};
export default SearchInput;
