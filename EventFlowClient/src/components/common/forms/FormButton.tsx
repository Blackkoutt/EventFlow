interface FormButtonProps {
  isSubmitting: boolean;
  text: string;
  background?: string;
  py?: number;
}

const FormButton = ({ isSubmitting, text, background = "#7B2CBF", py = 20 }: FormButtonProps) => {
  return (
    <button
      disabled={isSubmitting}
      style={{ background: background, paddingTop: `${py}px`, paddingBottom: `${py}px` }}
      className="rounded-md w-full text-white"
      type="submit"
    >
      {isSubmitting ? "Ładowanie..." : text}
    </button>
  );
};

export default FormButton;
