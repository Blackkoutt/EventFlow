import { useEffect, useState } from "react";
import { FieldError, FieldErrorsImpl, Merge, useFormContext } from "react-hook-form";
import { SelectOption } from "../../../helpers/SelectOptions";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faChevronUp } from "@fortawesome/free-solid-svg-icons";

export interface SelectProps extends React.SelectHTMLAttributes<HTMLSelectElement> {
  label: string;
  name: string;
  errorHeight?: number;
  isIcons?: boolean;
  maxHeight?: number;
  isEdit?: boolean;
  optionValues: SelectOption[];
  error: FieldError | Merge<FieldError, FieldErrorsImpl<any>> | undefined;
}

const Select = ({
  label,
  error,
  isIcons = false,
  name,
  errorHeight,
  maxHeight = 250,
  optionValues,
  ...props
}: SelectProps) => {
  const [isFocused, setIsFocused] = useState(false);
  const { register, getValues, setFocus, setValue } = useFormContext();

  const [selectedValue, setSelectedValue] = useState("");
  const [isOpen, setIsOpen] = useState(false);

  console.log(getValues(name) === "");

  useEffect(() => {
    if (optionValues.length > 0) {
      if (getValues(name) === "") {
        setValue(name, optionValues[0].value);
        setSelectedValue(optionValues[0].option);
      } else {
        setSelectedValue(getValues(name));
      }
    }
  }, []);

  useEffect(() => {
    if (isFocused) setFocus(name);
  }, [isFocused]);

  const handleSelect = (value: number | string, option: string) => {
    setValue(name, value);
    setSelectedValue(option);
    setIsOpen(false);
  };
  const errorMessage = error ? (error as FieldError)?.message : undefined;

  return (
    <div className="w-full">
      <div
        className={`w-full relative bg-[#ECECEC] h-[56px] rounded-md px-3
      flex flex-row justify-start items-start ${
        isFocused ? "outline outline-primaryPurple outline-2" : "outline-none"
      }`}
        tabIndex={1}
        onFocus={() => {
          setIsFocused(true);
        }}
        onBlur={() => {
          setIsFocused(false), setIsOpen(false);
        }}
        onClick={() => setIsOpen(!isOpen)}
      >
        <div className="w-full relative pt-6 pb-2 hover:cursor-pointer">
          <label
            htmlFor={name}
            className="absolute select-none -translate-y-4 translate-x-[2px] text-[12px] text-black hover:cursor-pointer"
          >
            {label}
          </label>

          <select
            {...register(name)}
            name={name}
            id={name}
            className="w-full hidden text-[#2F2F2F] font-semibold bg-[#ECECEC] hover:cursor-pointer"
            style={{ outline: "none" }}
            {...props}
            //onFocus={() => setIsFocused(true)}
          />
          <FontAwesomeIcon
            className={`absolute right-0 transition-all duration-300 ${
              isOpen ? "rotate-180" : "rotate-0"
            }`}
            icon={faChevronUp}
          />
          <div className="translate-x-[2px]">
            {isIcons ? (
              <div className="flex flex-row justify-start items-start gap-2 mt-[2px]">
                <i className={selectedValue}></i>
                <p>{selectedValue}</p>
              </div>
            ) : (
              selectedValue
            )}
          </div>
        </div>
        {isOpen && (
          <div
            className="select-items overflow-y-scroll grid grid-cols-5 gap-2 absolute left-0 right-0 top-full mt-1 bg-[#efefef] text-black rounded-lg shadow-lg z-10"
            style={{ maxHeight: maxHeight }}
            onClick={(e) => e.stopPropagation()}
          >
            {optionValues.map((optionValue) =>
              isIcons ? (
                <i
                  key={optionValue.value}
                  className={`${optionValue.value} p-3 text-[18px] rounded-lg cursor-pointer hover:bg-primaryPurple hover:text-white select-none`}
                  onClick={() => handleSelect(optionValue.value, optionValue.option)}
                ></i>
              ) : (
                <div
                  key={optionValue.value}
                  className="p-3 rounded-lg cursor-pointer hover:bg-primaryPurple hover:text-white select-none"
                  onClick={() => handleSelect(optionValue.value, optionValue.option)}
                >
                  {optionValue.option}
                </div>
              )
            )}
          </div>
        )}
      </div>
      <div style={{ minHeight: errorHeight }}>
        {errorMessage && <div className="text-red-500 text-[14.5px]">{errorMessage}</div>}
      </div>
    </div>
  );
};
export default Select;
