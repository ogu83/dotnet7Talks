public static class Startup
{
    private void ConfigureRateLimiters(ServiceConfigurationContext context) { context.Services.AddRateLimiter(limiterOptions => { limiterOptions.OnRejected = (context, cancellationToken) => { if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, outvarretryAfter)) { context.HttpContext.Response.Headers.RetryAfter = ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo); } context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests; context.HttpContext.RequestServices.GetService<ILoggerFactory>()?.CreateLogger("Microsoft.AspNetCore.RateLimitingMiddleware").LogWarning("OnRejected: {RequestPath}", context.HttpContext.Request.Path); returnnewValueTask(); }; limiterOptions.AddPolicy("UserBasedRateLimiting", context => { varcurrentUser = context.RequestServices.GetService<ICurrentUser>(); if (currentUserisnotnull && currentUser.IsAuthenticated) { returnRateLimitPartition.GetTokenBucketLimiter(currentUser.UserName, _ => newTokenBucketRateLimiterOptions{ TokenLimit = 10,QueueProcessingOrder = QueueProcessingOrder.OldestFirst,QueueLimit = 3,ReplenishmentPeriod = TimeSpan.FromMinutes(1),TokensPerPeriod = 4,AutoReplenishment = true}); }returnRateLimitPartition.GetSlidingWindowLimiter("anonymous-user", _ => newSlidingWindowRateLimiterOptions{ PermitLimit = 2,QueueProcessingOrder = QueueProcessingOrder.OldestFirst,QueueLimit = 1,Window = TimeSpan.FromMinutes(1),SegmentsPerWindow = 2}); });limiterOptions.GlobalLimiter=PartitionedRateLimiter.Create<HttpContext, string>(context=>{varcurrentTenant=context.RequestServices.GetService<ICurrentTenant>();if(currentTenantisnotnull&&currentTenant.IsAvailable){returnRateLimitPartition.GetConcurrencyLimiter(currentTenant!.Name,_=>newConcurrencyLimiterOptions{PermitLimit=5,QueueProcessingOrder=QueueProcessingOrder.OldestFirst,QueueLimit=1});}returnRateLimitPartition.GetNoLimiter("host");});});}
}