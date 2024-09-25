import { BaseEntity } from '../shared/baseEntity';

export class CategoryRequest extends BaseEntity {
  public title: string;
  public createdAt?: Date;

  constructor(title: string, id?: number) {
    super();
    this.title = title;
    this.id = id;
  }
}
