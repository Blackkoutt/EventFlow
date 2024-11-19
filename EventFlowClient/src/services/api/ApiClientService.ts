import { baseUrl } from "../../config/environment/Environment.ts";
import axios, { AxiosInstance } from "axios";
import { ApiModelConfig } from "../../config/ApiModelConfig";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum.ts";

export const api: AxiosInstance = axios.create({
  baseURL: baseUrl,
});

async function Get<TEntity>(model: ApiEndpoint, queryParams?: Record<string, any>) {
  try {
    const { url } = ApiModelConfig[model];

    const queryString = queryParams ? `?${new URLSearchParams(queryParams).toString()}` : "";

    const response = await api.get<TEntity>(url + queryString);
    return response.data;
  } catch (error) {
    console.error("Błąd przy wysyłaniu żądania GET:", error);
    throw error;
  }
}

const ApiMethod = {
  Get,
};

export default ApiMethod;
