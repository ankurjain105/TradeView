import { CommodityDataPoint } from './commodity-data-point';
import { CommodityRecentHistory } from './commodity-recent-history';

export function createCommodityDataPoint(): CommodityDataPoint {
  return {
    date: getRandomDate(),
    newTradeAction: getRandomNumber(),
    pnlDaily: getRandomNumber(),
    position: getRandomNumber(),
    price: getRandomNumber()
  };
}

export function createCommodityRecentHistory(): CommodityRecentHistory {
  return {
    commodity: 'commodity',
    model: 'model',
    dataPoints: [1,2,3].map(() => createCommodityDataPoint())
  };
}

export function getRandomDate(): Date {
  return new Date(
    getRandomNumber(2020, 2022),
    getRandomNumber(1, 12),
    getRandomNumber(1, 28));
}

export function getRandomNumber(min?: number, max?: number) : number{
	min = Math.ceil(min ?? -5000);
	max = Math.floor(max ?? 5000);
	return Math.floor(Math.random() * (max - min + 1)) + min;
}
