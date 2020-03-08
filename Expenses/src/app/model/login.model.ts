import { User } from '../model/user.model'

export interface Login {
  user: User,
  token: string
}
