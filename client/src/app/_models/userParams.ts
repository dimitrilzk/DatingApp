export class UserParams {
  gender: string;
  minAge = 18;
  maxAge = 99;
  pageNumbr = 1;
  pageSize = 5;

  constructor(user: User | null) {
    this.gender = user?.gender === 'female' ? 'male' : 'female';
  }
}
