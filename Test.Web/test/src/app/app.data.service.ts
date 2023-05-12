import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {Observable} from "rxjs";
import {Post} from "./post";
import {environment} from "../environments/environment";
import { ApiPaths } from 'src/enums/apiPaths';

@Injectable()
export class AppDataService {

  private url = environment.baseUrl + ApiPaths.Posts;

  constructor(private http: HttpClient) {  }

  get() : Observable<Post[]>  {
    return this.http.get<Post[]>(this.url);
  }

  getById(id: number) : Observable<Post>  {
    return this.http.get<Post>(this.url + '/' + id);
  }

  create(post: Post) {
    return this.http.post(this.url, post);
  }
}
