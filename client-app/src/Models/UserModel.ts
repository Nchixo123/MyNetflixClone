import { MovieModel } from "./MovieModel";

export interface UserModel {
    id: number;
    gender: number;  
    username: string;
    email: string;
    password: string;
    profilePictureUrl?: string;
    isDelete: boolean;
    favoriteMovies: MovieModel[];
}
