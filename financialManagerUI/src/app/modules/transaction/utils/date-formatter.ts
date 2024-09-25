import { format } from 'date-fns';

export const formatDate = (date: Date | string): string => {
  return format(date, 'PPpp');
};
