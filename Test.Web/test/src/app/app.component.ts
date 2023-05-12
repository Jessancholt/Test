import {Component, OnInit} from '@angular/core';
import {Post} from "./post";
import {AppDataService} from "./app.data.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  post: Post = new Post();
  posts: Post[] = new Array();
  temp: Post[] = new Array();
  id!: number;
  listMode: boolean = true;
  isFindButtonPressed: boolean = false;

  constructor(private dataService: AppDataService) {
  }

  ngOnInit() {
    this.get();
  }

  get() {
    this.dataService.get().subscribe(x => this.posts = x);
  }

  getById(id: number) {
    this.temp = this.posts;
    this.posts = new Array();
    this.isFindButtonPressed = true;
    this.dataService.getById(id).subscribe(x => this.posts.unshift(x));
  }

  add() {
    this.dataService.create(this.post).subscribe(() => {
      this.posts.push(this.post);
      this.post = new Post();
    });
    this.listMode = true;
  }

  cancel() {
    this.post = new Post();
    this.listMode = true;
  }

  getHostname(url: string) {
    if (url === null || url === undefined)
      return;

    return new URL(url).hostname;
  }

  checkValue() {
    if (this.isFindButtonPressed && this.id === null) {
      this.posts = this.temp;
      this.isFindButtonPressed = false;
    }
  }

  checkUrl() {
    try {
      new URL(this.post.url!);
    } catch (error) {
      return false;
    }
    return true;
  }

  checkTime() {
    if (isNaN(Date.parse(this.post.time!)))
      return false;
    else
      return true;
  }

  validateAll(){

  }
}
