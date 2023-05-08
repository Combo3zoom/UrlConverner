import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-short-url-details',
  templateUrl: './short-url-details.component.html',
  styleUrls: ['./short-url-details.component.css']
})
export class ShortUrlDetailsComponent implements OnInit {
  id: number = 0;
  description: string = '';
  userRole: string | null = '';

  constructor(private http: HttpClient, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = +(this?.route?.snapshot?.paramMap?.get('id') ?? 0);

    this.userRole = localStorage.getItem('userRole');

    this.loadDescription();
  }

  loadDescription(): void {
    this.http.get<{ description: string }>(`https://localhost:7009/api/short-urls/${this.id}/description`).subscribe(
      (response) => {
        console.log("description:", this.description)
        this.description = response.description;
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }

  updateDescription(): void {
    this.http.put(`https://localhost:7009/api/short-urls/${this.id}/description`, { description: this.description },
      { withCredentials: true, headers: { 'Content-Type': 'application/json' } }).subscribe(
      () => {
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }
}
