import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useRef, useState } from "react";
import { faEye, faEyeSlash } from "@fortawesome/free-solid-svg-icons";

export interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  name: string;
  icon?: IconDefinition;
  iconwidth?: number;
  iconHeight?: number;
}

const Input = React.forwardRef<HTMLInputElement, InputProps>(
  (
    { label, icon, name, iconwidth = 24, iconHeight = 24, onChange, type = "text", ...props },
    ref
  ) => {
    const [isFocused, setIsFocused] = useState(false);
    const inputValue = useRef<string>("");
    const [showPassword, setShowPassword] = useState(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      inputValue.current = e.target.value;
      if (onChange) {
        onChange(e);
      }
    };

    return (
      <div
        className={`w-full bg-[#ECECEC] rounded-md px-3 flex flex-row justify-start items-center gap-3 ${
          isFocused ? "outline outline-primaryPurple outline-2" : "outline-none"
        }`}
      >
        {icon ? (
          <FontAwesomeIcon
            icon={icon}
            style={{ color: `black`, width: iconwidth, height: iconHeight }}
          />
        ) : null}
        <div className="w-full relative pt-6 pb-2 ">
          <label
            htmlFor={name}
            className={`absolute transition-all text-[#2F2F2F] duration-300 ${
              isFocused || inputValue.current
                ? "-translate-y-4 text-[12px] text-black"
                : "-translate-y-[12px] text-lg"
            }`}
          >
            {label}
          </label>

          <input
            ref={ref}
            name={name}
            id={name}
            type={type === "password" && showPassword ? "text" : type}
            className="w-full text-[#2F2F2F] font-semibold bg-[#ECECEC]"
            style={{ outline: "none" }}
            {...props}
            onChange={handleChange}
            onFocus={() => setIsFocused(true)}
            onBlur={() => setIsFocused(false)}
          />
        </div>
        {type === "password" ? (
          <FontAwesomeIcon
            icon={showPassword ? faEye : faEyeSlash}
            style={{ color: `black`, width: iconwidth, height: iconHeight, cursor: "pointer" }}
            onClick={() => setShowPassword(!showPassword)}
          />
        ) : null}
      </div>
    );
  }
);

export default Input;
