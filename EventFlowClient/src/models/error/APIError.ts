export type APIError = {
  code: number;
  title: string;
  details: ErrorDetails;
  type: string;
};

type ErrorDetails = {
  errors: string[];
};
