import { baseUrl } from "../../environment/environment";
import axios, { AxiosInstance } from "axios";
import { AdditionalServices } from "../../models/response_models/AdditionalServices";

export const api: AxiosInstance = axios.create({
  baseURL: baseUrl,
});

export enum Model {
  AdditionalServices,
}

const modelConfig = {
  [Model.AdditionalServices]: {
    url: "/additionalservices",
    modelType: {} as AdditionalServices,
  },
};

async function Get(model: Model) {
  try {
    const { url, modelType } = modelConfig[model];
    const response = await api.get<typeof modelType>(url);
    return response.data;
  } catch (error) {
    console.error("Błąd przy wysyłaniu żądania GET:", error);
    throw error;
  }
}

const Api = {
  Get,
};

export default Api;
