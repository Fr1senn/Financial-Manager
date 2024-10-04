type SortOption = {
  fieldName: string;
  ascending: boolean;
};

type Filter = {
  key: string;
  value: string;
};

export class PageRequest {
  public take: number;
  public skip: number;
  public filters?: Filter[] = undefined;
  public sortOptions?: SortOption[] = undefined;

  constructor(
    take: number,
    skip: number,
    filterOptions?: Filter[],
    sortOptions?: SortOption[]
  ) {
    this.take = take;
    this.skip = skip;
    this.filters = filterOptions;
    this.sortOptions = sortOptions;
  }
}
