import { useEffect, useState } from "react";

interface RadioButtonProps {
  label: string;
  id: string;
  value: string | number;
  isChecked?: boolean;
  onChecked: (value: string | number) => void;
}

const RadioButton = ({ label, id, value, isChecked = false, onChecked }: RadioButtonProps) => {
  const [checked, setChecked] = useState(isChecked);

  useEffect(() => {
    if (checked) {
      onChecked(value);
    }
  }, [checked]);

  return (
    <div
      className="relative flex flex-row items-center gap-2"
      tabIndex={0}
      onFocus={() => setChecked(true)}
      onBlur={() => setChecked(false)}
    >
      <input type="radio" readOnly id={id} className="peer hidden" checked={checked} />
      <div
        className={`w-5 h-5 border-2 ${
          checked ? "border-[#7B2CBF]" : "border-gray-400"
        }  rounded-full cursor-pointer flex items-center justify-center`}
      >
        <span
          className={`w-[12px] h-[12px] rounded-full ${
            checked ? "bg-[#7B2CBF]" : "bg-transparent"
          }`}
        ></span>
      </div>
      <label className="hover:cursor-pointer select-none" htmlFor={id}>
        {label}
      </label>
    </div>
  );
};
export default RadioButton;
