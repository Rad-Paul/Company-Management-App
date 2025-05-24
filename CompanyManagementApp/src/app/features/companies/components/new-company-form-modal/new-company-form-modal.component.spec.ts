import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCompanyFormModalComponent } from './new-company-form-modal.component';

describe('NewCompanyFormModalComponent', () => {
  let component: NewCompanyFormModalComponent;
  let fixture: ComponentFixture<NewCompanyFormModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewCompanyFormModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NewCompanyFormModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
