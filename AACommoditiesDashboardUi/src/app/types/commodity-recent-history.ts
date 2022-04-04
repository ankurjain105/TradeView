import { CommodityDataPoint } from "./commodity-data-point";

export interface CommodityRecentHistory {
  model: string;
  commodity: string;
  dataPoints: CommodityDataPoint[];
}
