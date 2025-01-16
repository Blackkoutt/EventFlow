import { MutableRefObject } from "react";
import { toast } from "react-toastify";

interface TransactionActionProps {
  key: string;
  hasSuccessRef: MutableRefObject<boolean>;
  hasErrorRef: MutableRefObject<boolean>;
  promise: Promise<void>;
  toastMessage: {
    pending: string;
    success: string;
    error: string;
  };
}

export const TransactionAction = async ({
  key,
  hasSuccessRef,
  hasErrorRef,
  promise,
  toastMessage,
}: TransactionActionProps) => {
  const searchParams = new URLSearchParams(window.location.search);
  console.log("Error", searchParams.has("error"));
  console.log("Query", searchParams.has(key));
  console.log(hasSuccessRef);
  console.log(hasSuccessRef.current);
  console.log(hasErrorRef);
  console.log(hasErrorRef.current);

  if (searchParams.has("error") && !hasErrorRef.current) {
    hasErrorRef.current = true;
    toast.error("Transakcja została anulowana");
    const url = new URL(window.location.toString());
    url.searchParams.delete("error");
    url.searchParams.delete(key);
    window.history.replaceState({}, "", url.toString());
  } else if (searchParams.has(key) && !hasSuccessRef.current && !hasErrorRef.current) {
    hasSuccessRef.current = true;
    console.log("hello");
    console.log(promise);
    await toast.promise(promise, {
      pending: toastMessage.pending,
      success: toastMessage.success,
      error: toastMessage.error,
    });
    const url = new URL(window.location.toString());
    url.searchParams.delete(key);
    window.history.replaceState({}, "", url.toString());
  }
};
