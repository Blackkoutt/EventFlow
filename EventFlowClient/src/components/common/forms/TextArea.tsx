import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { FormEvent, useEffect, useState } from "react";
import { FieldError, FieldErrorsImpl, Merge, useFormContext } from "react-hook-form";

export interface TextAreaProps extends React.TextareaHTMLAttributes<HTMLTextAreaElement> {
  label: string;
  name: string;
  error: FieldError | Merge<FieldError, FieldErrorsImpl<any>> | undefined;
  isFirstLetterUpperCase?: boolean;
  icon?: IconDefinition;
  iconwidth?: number;
  onInput?: (e: FormEvent<HTMLTextAreaElement>) => void;
  minLength?: number;
  errorHeight?: number;
  maxLength?: number;
  iconHeight?: number;
  maxHeight?: number;
  minHeight?: number;
}

const TextArea = ({
  label,
  icon,
  error,
  isFirstLetterUpperCase = false,
  name,
  maxLength,
  minLength,
  onInput,
  iconwidth = 24,
  iconHeight = 24,
  errorHeight,
  maxHeight = 150,
  minHeight = 24,
  onChange,
  ...props
}: TextAreaProps) => {
  const [isFocused, setIsFocused] = useState(false);
  const [textLength, setTextLength] = useState(0);
  const { register, getValues, setFocus } = useFormContext();

  useEffect(() => {
    if (isFocused) setFocus(name);
  }, [isFocused]);

  const errorMessage = error ? (error as FieldError)?.message : undefined;

  const capitalizeFirstLetter = (e: FormEvent<HTMLTextAreaElement>) => {
    const input = e.target as HTMLTextAreaElement;
    if (input.value && /^[a-z]/i.test(input.value)) {
      input.value = input.value.charAt(0).toUpperCase() + input.value.slice(1);
    }
  };

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
        <div className="w-full relative pt-6 pb-2">
          <label
            htmlFor={name}
            className={`absolute transition-all text-[#2F2F2F] duration-300 cursor-text ${
              isFocused || getValues(name)
                ? "-translate-y-[18px] text-[12px] text-black"
                : "-translate-y-[10px] text-lg"
            }`}
          >
            {label}
          </label>

          <textarea
            {...register(name)}
            name={name}
            id={name}
            maxLength={maxLength}
            minLength={minLength}
            className="w-full text-[#2F2F2F] font-semibold bg-[#ECECEC] h-[24px]"
            style={{ outline: "none", maxHeight: maxHeight, minHeight: minHeight }}
            {...props}
            onInput={(e) => {
              if (isFirstLetterUpperCase) capitalizeFirstLetter(e);
              const input = e.target as HTMLTextAreaElement;
              setTextLength(input.value.length);
            }}
            onFocus={() => setIsFocused(true)}
            onBlur={() => setIsFocused(false)}
          />
          <div className="absolute text-[10px] bottom-[2px] right-[18px]">{`${textLength}/${maxLength}`}</div>
        </div>
      </div>
      <div style={{ height: errorHeight !== undefined ? `${errorHeight}px` : "auto" }}>
        {errorMessage && <div className="text-red-500 text-[14.5px]">{errorMessage}</div>}
      </div>
    </div>
  );
};

export default TextArea;
