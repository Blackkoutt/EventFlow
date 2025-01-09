import { useEffect, useRef, useState } from "react";

export const useDialogs = <TEntity>() => {
  const deleteDialog = useRef<HTMLDialogElement>(null);
  const detailsDialog = useRef<HTMLDialogElement>(null);
  const modifyDialog = useRef<HTMLDialogElement>(null);
  const createDialog = useRef<HTMLDialogElement>(null);
  const downloadDialog = useRef<HTMLDialogElement>(null);

  const [itemToDelete, setItemToDelete] = useState<TEntity | undefined>(undefined);
  const [itemToDetails, setItemToDetails] = useState<TEntity | undefined>(undefined);
  const [itemToModify, setItemToModify] = useState<TEntity | undefined>(undefined);
  const [itemToDownload, setItemToDownload] = useState<TEntity | undefined>(undefined);

  useEffect(() => {
    if (itemToDelete != undefined) {
      deleteDialog.current?.showModal();
    }
  }, [itemToDelete]);

  useEffect(() => {
    if (itemToDetails != undefined) {
      detailsDialog.current?.showModal();
    }
  }, [itemToDetails]);

  useEffect(() => {
    if (itemToModify != undefined) {
      modifyDialog.current?.showModal();
    }
  }, [itemToModify]);

  useEffect(() => {
    if (itemToDownload != undefined) {
      downloadDialog.current?.showModal();
    }
  }, [itemToDownload]);

  const onDialogClose = () => {
    deleteDialog.current?.close();
    modifyDialog.current?.close();
    detailsDialog.current?.close();
    createDialog.current?.close();
    downloadDialog.current?.close();
    setItemToDelete(undefined);
    setItemToDetails(undefined);
    setItemToModify(undefined);
    setItemToDownload(undefined);
  };

  const onDelete = (rowData: TEntity) => {
    setItemToDelete(rowData);
    deleteDialog.current?.showModal();
  };

  const onModify = (rowData: TEntity) => {
    setItemToModify(rowData);
    modifyDialog.current?.showModal();
  };

  const onDetails = (rowData: TEntity) => {
    setItemToDetails(rowData);
    detailsDialog.current?.showModal();
  };

  const onDownload = (rowData: TEntity) => {
    setItemToDownload(rowData);
    downloadDialog.current?.showModal();
  };

  const onCreate = () => {
    createDialog.current?.showModal();
  };

  const closeDialogsAndSetValuesToDefault = () => {
    deleteDialog.current?.close();
    modifyDialog.current?.close();
    detailsDialog.current?.close();
    createDialog.current?.close();
    downloadDialog.current?.close();
    setItemToDelete(undefined);
    setItemToModify(undefined);
    setItemToDetails(undefined);
    setItemToDownload(undefined);
  };

  return {
    deleteDialog,
    createDialog,
    detailsDialog,
    modifyDialog,
    downloadDialog,
    itemToDelete,
    itemToDetails,
    itemToModify,
    itemToDownload,
    onDialogClose,
    onDelete,
    onModify,
    onCreate,
    onDetails,
    onDownload,
    closeDialogsAndSetValuesToDefault,
  };
};
