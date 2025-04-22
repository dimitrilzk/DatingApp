export interface User {
  //al posto di interface si puo usare anche export type User = {...}
  username: string;
  knownAs: string;
  gender: string;
  token: string;
  photoUrl?: string;
}
