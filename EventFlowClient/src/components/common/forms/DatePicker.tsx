import React, { useEffect, useInsertionEffect, useRef, useState } from "react";
import { faCalendar } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { FieldError, useFormContext } from "react-hook-form";
import { userRegisterSchema } from "../../../models/create_schemas/auth/UserRegisterSchema";

export interface DatePickerProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  name: string;
  error: FieldError | undefined;
  errorHeight?: number;
}

const DatePicker = React.forwardRef<HTMLInputElement, DatePickerProps>(
  ({ label, name, error, errorHeight, type = "text", ...props }, ref) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedDate, setSelectedDate] = useState<Date | null>(null);
    const [currentMonth, setCurrentMonth] = useState(new Date());

    const { setValue, register } = useFormContext();

    useEffect(() => {
      if (selectedDate !== null) setValue(name, selectedDate.toLocaleDateString());
    }, [selectedDate]);

    const days = ["pon", "wto", "śro", "czw", "pią", "sob", "nie"];
    const daysInMonth = (year: number, month: number) => new Date(year, month + 1, 0).getDate();

    const firstDayOfMonth = new Date(
      currentMonth.getFullYear(),
      currentMonth.getMonth(),
      1
    ).getDay();
    const totalDays = daysInMonth(currentMonth.getFullYear(), currentMonth.getMonth());

    const addCount = firstDayOfMonth != 0 ? firstDayOfMonth - 1 : 6;

    const gridCells = [
      ...Array(addCount).fill(null),
      ...Array.from({ length: totalDays }, (_, i) => i + 1),
    ];

    while (gridCells.length % 7 !== 0) {
      gridCells.push(null);
    }

    const handleDateClick = (day: number) => {
      const newDate = new Date(currentMonth.getFullYear(), currentMonth.getMonth(), day);
      setSelectedDate(newDate);
    };

    const handleChange = (offset: number, monthOrYear: string) => {
      let newMonth: Date | undefined = undefined;
      switch (monthOrYear) {
        case "month":
          newMonth = new Date(currentMonth.getFullYear(), currentMonth.getMonth() + offset);
          break;
        case "year":
          newMonth = new Date(currentMonth.getFullYear() + offset, currentMonth.getMonth());
          break;
      }
      if (newMonth !== undefined) {
        setCurrentMonth(newMonth);
        const day = selectedDate?.getDate();
        const newDate = new Date(newMonth.getFullYear(), newMonth.getMonth(), day);

        if (!isNaN(newDate.getTime())) {
          setSelectedDate(newDate);
        }
      }
    };

    return (
      <div className="w-full">
        <div
          className={`w-full bg-[#ECECEC] rounded-md px-3 flex flex-row justify-start items-center gap-3 ${
            isOpen ? "outline outline-primaryPurple outline-2" : "outline-none"
          }`}
          onClick={() => setIsOpen(true)}
        >
          <FontAwesomeIcon
            icon={faCalendar}
            style={{ color: `black`, width: "24px", height: "24px" }}
          />
          <div className="w-full relative pt-6 pb-2 ">
            <label
              htmlFor={name}
              className="absolute transition-all text-[#2F2F2F] duration-300 -translate-y-4 text-[12px]"
            >
              {label}
            </label>
            <input
              {...register(name)}
              //ref={ref}
              name={name}
              id={name}
              type="text"
              className="w-full text-[#2F2F2F] font-semibold bg-[#ECECEC] cursor-pointer"
              style={{ outline: "none" }}
              {...props}
              value={selectedDate ? selectedDate.toLocaleDateString() : ""}
              readOnly
            />
            {isOpen && (
              <div
                className="absolute top-[101.2%] left-0 bg-white p-3 drop-shadow-xl z-10"
                onBlur={() => setIsOpen(false)}
                tabIndex={0}
              >
                <div className="flex flex-row items-center justify-between mb-3">
                  <div
                    className="bg-primaryPurple text-white px-[22px] py-[10px] text-base rounded-md text-center cursor-pointer"
                    onClick={() => handleChange(-1, "month")}
                  >
                    {"<"}
                  </div>
                  <div className="flex flex-row items-center justify-center gap-1 font-semibold">
                    <p>{currentMonth.toLocaleString("default", { month: "long" })}</p>
                    <p>{currentMonth.getFullYear()}</p>
                    <div className="flex flex-col justify-center items-center gap-[2px] pl-2">
                      <div
                        className="bg-primaryPurple text-white px-[7px] py-[4px] text-[13px] rounded-md text-center cursor-pointer"
                        onClick={() => handleChange(1, "year")}
                      >
                        {"↑"}
                      </div>
                      <div
                        className="bg-primaryPurple text-white px-[7px] py-[4px] text-[13px] rounded-md text-center cursor-pointer"
                        onClick={() => handleChange(-1, "year")}
                      >
                        {"↓"}
                      </div>
                    </div>
                  </div>
                  <div
                    className="bg-primaryPurple text-white px-[22px] py-[10px] text-base rounded-md text-center cursor-pointer"
                    onClick={() => handleChange(1, "month")}
                  >
                    {">"}
                  </div>
                </div>
                <div className="grid gap-[5px]" style={{ gridTemplateColumns: "repeat(7, 1fr)" }}>
                  {days.map((day, index) => (
                    <div className="text-center" key={index}>
                      {day}
                    </div>
                  ))}
                  {gridCells.map((day, index) => (
                    <div
                      key={index}
                      className="p-[10px] border-[1px] border-[#ddd] text-center"
                      style={{
                        cursor: day ? "pointer" : "default",
                        backgroundColor: day
                          ? selectedDate?.getDate() === day
                            ? "#7B2CBF"
                            : "white"
                          : "#efefef",
                        color: day && selectedDate?.getDate() === day ? "white" : "black",
                      }}
                      onClick={day ? () => handleDateClick(day) : undefined}
                    >
                      {day ? day : ""}
                    </div>
                  ))}
                </div>
              </div>
            )}
          </div>
        </div>
        <div style={{ height: errorHeight !== undefined ? `${errorHeight}px` : "auto" }}>
          {error && <div className="text-red-500 text-[14.5px]">{error.message}</div>}
        </div>
      </div>
    );
  }
);

export default DatePicker;
