import { baseUrl } from "../../config/environment/Environment.ts";
import axios, { AxiosInstance } from "axios";
import { ApiUrlConfig } from "../../config/ApiUrlConfig.ts";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum.ts";

export const api: AxiosInstance = axios.create({
  baseURL: baseUrl,
  withCredentials: true,
});

async function Get<TEntity>(endpoint: ApiEndpoint, queryParams?: Record<string, any>) {
  try {
    const { url } = ApiUrlConfig[endpoint];

    const queryString = queryParams ? `?${new URLSearchParams(queryParams).toString()}` : "";

    const response = await api.get<TEntity>(url + queryString, { withCredentials: true });
    return response.data;
  } catch (error) {
    throw error;
  }
}

async function Post<TEntity, TPostEntity>(endpoint: ApiEndpoint, body: TPostEntity) {
  try {
    const { url } = ApiUrlConfig[endpoint];

    const response = await api.post<TEntity>(url, body);
    const status = response.status;
    const data = response.data;
    return [data];
  } catch (error) {
    throw error;
  }
}

async function Put<TEntity, TPutEntity>(endpoint: ApiEndpoint, id: number, body: TPutEntity) {
  try {
    const { url } = ApiUrlConfig[endpoint];
    const fullUrl = `${url}/${id}`;

    const response = await api.put<TEntity>(fullUrl, body);
    return [response.data];
  } catch (error) {
    throw error;
  }
}

async function Delete<TEntity>(endpoint: ApiEndpoint, id: number) {
  try {
    const { url } = ApiUrlConfig[endpoint];
    const fullUrl = `${url}/${id}`;

    const response = await api.delete<TEntity>(fullUrl);
    return response.data;
  } catch (error) {
    throw error;
  }
}

const GetPhotoEndpoint = (photoEndpoint: string): string => {
  return `${baseUrl}${photoEndpoint}`;
};

const ApiMethod = {
  Get,
  GetPhotoEndpoint,
  Post,
  Put,
  Delete,
};

export default ApiMethod;
