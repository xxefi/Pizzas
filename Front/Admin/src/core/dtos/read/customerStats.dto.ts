import type { CustomerSegment } from "./customerSegment.dto";

export interface CustomerStatsDto {
  totalCustomers: number;
  newCustomers: number;
  returningCustomers: number;
  averageCustomerValue: number;
  segments: CustomerSegment[];
}
