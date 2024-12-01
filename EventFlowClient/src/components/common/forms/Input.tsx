import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { useEffect, useRef, useState } from "react";
import { faEye, faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { FieldError, useFormContext } from "react-hook-form";

export interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  name: string;
  error: FieldError | undefined;
  icon?: IconDefinition;
  iconwidth?: number;
  iconHeight?: number;
}

const Input = ({
  label,
  icon,
  error,
  name,
  iconwidth = 24,
  iconHeight = 24,
  onChange,
  type = "text",
  ...props
}: InputProps) => {
  const [isFocused, setIsFocused] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const { register, getValues, setFocus } = useFormContext();

  useEffect(() => {
    if (isFocused) setFocus(name);
  }, [isFocused]);

  return (
    <div className="w-full">
      <div
        className={`w-full bg-[#ECECEC] rounded-md px-3
            cursor-text
            flex flex-row justify-start items-center gap-3 ${
              isFocused ? "outline outline-primaryPurple outline-2" : "outline-none"
            }`}
        tabIndex={1}
        onFocus={() => setIsFocused(true)}
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
            className={`absolute transition-all text-[#2F2F2F] duration-300 cursor-text ${
              isFocused || getValues(name)
                ? "-translate-y-4 text-[12px] text-black"
                : "-translate-y-[10px] text-lg"
            }`}
          >
            {label}
          </label>

          <input
            {...register(name)}
            name={name}
            id={name}
            type={type === "password" && showPassword ? "text" : type}
            className="w-full text-[#2F2F2F] font-semibold bg-[#ECECEC]"
            style={{ outline: "none" }}
            {...props}
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
      {error && <div className="text-red-500">{error.message}</div>}
    </div>
  );
};

export default Input;
